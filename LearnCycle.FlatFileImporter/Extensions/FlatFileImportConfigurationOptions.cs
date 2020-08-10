namespace LearnCycle.FlatFileImporter.Extensions
{
    public class FlatFileImportConfigurationOptions
    {
        public bool IsActive { get; set; }
        public string StorageType { get; set; }
        public string AzureBlobConnection { get; set; }
        public string QueueName { get; set; }
    }
}
