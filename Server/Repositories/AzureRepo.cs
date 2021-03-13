using Azure.Storage;
using Azure.Storage.Blobs;
using Learning.Server.DbContext;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Server.Repositories {
    public interface IAzureRepo {
        Task<Uri> UploadFileToStorage(Stream stream, string container, string fileName);
    }

    public class AzureRepo : IAzureRepo {
        readonly IConfiguration _config;
        private readonly AppDbContext _dbContext;
        public AzureRepo(AppDbContext dbContext, IConfiguration config) {
            _dbContext = dbContext;
            _config = config;
        }

        public async Task<Uri> UploadFileToStorage(Stream stream, string container, string fileName) {
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
    }
}
