using LearnCycle.FlatFileImporter.Abstractions;
using Microsoft.Extensions.Configuration;
using System;

namespace LearnCycle.FlatFileImporter.Extensions
{
    public class FlatFileImportConfiguration : IFlatFileImportConfiguration
    {
        private bool IsActive { get; }
        private string StorageType { get; }
        private string AzureBlobConnection { get; }

        public FlatFileImportConfiguration(IConfiguration configuration)
        {
            var featureOptions = configuration.GetSection("FlatFileImport")
                .Get<FlatFileImportConfigurationOptions>() ?? throw new Exception("FlatFileImport not configured");
            IsActive = featureOptions.IsActive;
            StorageType = featureOptions.StorageType;
            AzureBlobConnection = featureOptions.AzureBlobConnection;
        }

        public bool FlatFileImportIsActive()
        {
            return IsActive;
        }

        public string GetStorageType()
        {
            return StorageType;
        }

        public string GetAzureBlobConnection()
        {
            return AzureBlobConnection;
        }
    }
}
