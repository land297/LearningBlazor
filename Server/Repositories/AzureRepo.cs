using Azure.Storage;
using Azure.Storage.Blobs;
using Learning.Server.DbContext;
using Learning.Server.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IAzureRepo {
        Task<Uri> NewBlobFromStream(Stream stream, string container, string fileName);
        Task<Uri> GetSasUriForBlob(Uri uri);
    }

    public class AzureRepo : IAzureRepo {
        readonly IConfiguration _config;
        
        
        public AzureRepo( IConfiguration config) {
           
            _config = config;
           
        }

        public async Task<Uri> NewBlobFromStream(Stream stream, string container, string fileName) {
            var accuntName = _config["AppSettings:AzureAccountName"];
            var key = _config["AppSettings:AzureAccountKey"];

            Uri blobUri = new Uri("https://" +
                                  accuntName +
                                  ".blob.core.windows.net/" +
                                  container + "/" + fileName);

            StorageSharedKeyCredential storageCredentials =
                new StorageSharedKeyCredential(accuntName, key);

            // Create the blob client.
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            
            // Upload the file
            await blobClient.UploadAsync(stream, true);

            return blobUri;
        }

        public async Task<Uri> GetSasUriForBlob(Uri uri) {
            var accuntName = _config["AppSettings:AzureAccountName"];
            var key = _config["AppSettings:AzureAccountKey"];

            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential(accuntName, key);
            
            // Create the blob client.
            BlobClient blobClient = new BlobClient(uri, storageCredentials);
            await Task.Delay(0);
            return blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, DateTimeOffset.Now.AddHours(1));
        }
    }
}
