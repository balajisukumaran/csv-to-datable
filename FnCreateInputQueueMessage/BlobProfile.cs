using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;

namespace FnCreateInputQueueMessage
{
    public class BlobProfile
    {
        string connectionString ;
        BlobServiceClient blobServiceClient;
        BlobContainerClient containerClient;
        BlobClient blobClient;
        public BlobProfile(String container, string inputFile)
        {
            connectionString = "DefaultEndpointsProtocol=https;AccountName=rbcdemoadfstorageaccount;AccountKey=PxO5oZJVQ9z6RklsO774U4KVmewkC7zhTcBdFZXfc9D838OKSaTCP2v21piDlmDc+/yguo/hezBd6fVqdToZIw==;EndpointSuffix=core.windows.net";
            blobServiceClient = new BlobServiceClient(connectionString);
            containerClient = blobServiceClient.GetBlobContainerClient(container);
            blobClient = containerClient.GetBlobClient(inputFile);
        }

        public async System.Threading.Tasks.Task<StreamReader> ListBlobsAsync()
        {
            var response = await blobClient.DownloadAsync();
            return new StreamReader(response.Value.Content);            
        }
    }
}
