namespace LearnCycle.FlatFileImporter.Abstractions
{
    public interface IFlatFileImportConfiguration
    {
        bool FlatFileImportIsActive();
        string GetStorageType();
        string GetAzureBlobConnection();
    }
}
