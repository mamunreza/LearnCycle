using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                using var scope = _serviceScopeFactory.CreateScope();

                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var fileProcessor = scope.ServiceProvider.GetRequiredService<IStreamProcessor>();

                fileProcessor.Source = configuration["FilePath:Source"];
                fileProcessor.Destination = Path.Combine(configuration["FilePath:Destination"], $"{DateTimeOffset.Now.Ticks.ToString()}.txt");
                fileProcessor.Process();
                _logger.LogInformation("Worker completed at: {time}", DateTimeOffset.Now);

                await Task.Delay(5000, cancellationToken);
                //await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
            }
        }
    }
}
