namespace LearnCycle.FlatFileImporter
{
    public interface IStreamProcessor
    {
        string Source { get; set; }
        string Destination { get; set; }
        bool Process();
        bool ProcessFromBlob();
    }
}
