namespace LearnCycle.FlatFileImporter.Abstractions
{
    public interface IBlobService
    {
        void Read(string containerName, string fileName);
    }
}
