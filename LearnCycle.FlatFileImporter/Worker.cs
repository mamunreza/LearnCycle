using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LearnCycle.FlatFileImporter.Abstractions;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LearnCycle.FlatFileImporter
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                IServiceScope scope = await ImportFileFromBlob(cancellationToken);
            }
        }

        private async Task<IServiceScope> ImportFileFromBlob(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker running at: {time} for Blob file import", DateTimeOffset.Now);
            var scope = _serviceScopeFactory.CreateScope();

            var flatFileConfiguration = scope.ServiceProvider.GetRequiredService<IFlatFileImportConfiguration>();
            string destinationFile = Path.Combine(@"C:\\temp\\FileProcess\\Destination", "DOWNLOADED.txt");

            var container = await GetContainerAsync("learn-cycle", flatFileConfiguration);
            var blobs = container.ListBlobs().OfType<CloudBlockBlob>().ToList();
            if (blobs.Count > 0)
            {
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference("Tasks.txt");
                Console.WriteLine("\nDownloading blob to\n\t{0}\n", destinationFile);
                await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);
            }
            await Task.Delay(5000, cancellationToken);

            return scope;
        }

        private async Task<CloudBlobContainer> GetContainerAsync(string containerName, IFlatFileImportConfiguration configuration)
        {
            var storageAccount = CloudStorageAccount.Parse(configuration.GetAzureBlobConnection());
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            try
            {
                await container.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Azure container {ContainerName} cannot be created", containerName);
            }

            return container;
        }

        private async Task<IServiceScope> ImportLocalFile(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var scope = _serviceScopeFactory.CreateScope();

            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var fileProcessor = scope.ServiceProvider.GetRequiredService<IStreamProcessor>();

            fileProcessor.Source = configuration["FilePath:Source"];
            fileProcessor.Destination = Path.Combine(configuration["FilePath:Destination"], $"{DateTimeOffset.Now.Ticks.ToString()}.txt");
            fileProcessor.Process();
            _logger.LogInformation("Worker completed at: {time}", DateTimeOffset.Now);

            await Task.Delay(5000, cancellationToken);
            return scope;
        }
    }
}
