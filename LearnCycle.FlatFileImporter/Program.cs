using LearnCycle.FlatFileImporter.Abstractions;
using LearnCycle.FlatFileImporter.Extensions;
using LearnCycle.FlatFileImporter.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LearnCycle.FlatFileImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(config => config.AddUserSecrets(Assembly.GetExecutingAssembly()))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<IFlatFileImportConfiguration, FlatFileImportConfiguration>();
                    services.AddScoped<IBlobService, BlobService>();

                    services.AddScoped<IStreamProcessor, FileProcessor>();

                    services.AddHostedService<Worker>();
                });
    }
}
