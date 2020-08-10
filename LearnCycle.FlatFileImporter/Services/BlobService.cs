using LearnCycle.FlatFileImporter.Abstractions;
using LearnCycle.FlatFileImporter.Extensions;
using System;

namespace LearnCycle.FlatFileImporter.Services
{
    public class BlobService : IBlobService
    {
        public void Read(string containerName, string fileName)
        {
            
            //string connectionString = $"yourconnectionstring";

            //// Setup the connection to the storage account
            //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            //// Connect to the blob storage
            //CloudBlobClient serviceClient = storageAccount.CreateCloudBlobClient();
            //// Connect to the blob container
            //CloudBlobContainer container = serviceClient.GetContainerReference($"{containerName}");
            //// Connect to the blob file
            //CloudBlockBlob blob = container.GetBlockBlobReference($"{fileName}");
            //// Get the blob file as text
            //string contents = blob.DownloadTextAsync().Result;

            //return contents;
        }
    }
}
