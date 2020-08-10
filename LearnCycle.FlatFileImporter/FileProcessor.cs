using System;
using System.IO;

namespace LearnCycle.FlatFileImporter
{
    public class FileProcessor : IStreamProcessor
    {
        public string Source { get; set; }
        public string Destination { get; set; }

        public bool Process()
        {
            Validate();

            using (StreamReader sr = new StreamReader(Source))
                StreamInDestination(Destination, sr);

            return true;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Source))
                throw new Exception("Source path is not set for file operation");

            if (string.IsNullOrEmpty(Destination))
                throw new Exception("Destination path is not set for file operation");

            if (!File.Exists(Source))
                throw new FileNotFoundException("Source file missing");
        }

        private void StreamInDestination(string destination, StreamReader sr)
        {
            using FileStream fs = new FileStream(destination, FileMode.Append, FileAccess.Write);
            using StreamWriter sw = new StreamWriter(fs);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sw.WriteLine(line);
            }
        }

        public bool ProcessFromBlob()
        {
            throw new NotImplementedException();
        }
    }
}
