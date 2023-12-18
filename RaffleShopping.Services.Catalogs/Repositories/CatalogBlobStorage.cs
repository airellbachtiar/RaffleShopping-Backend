using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using RaffleShopping.Services.Catalogs.Models;

namespace RaffleShopping.Services.Catalogs.Repositories
{
    public class CatalogBlobStorage : ICatalogBlobStorage
    {
        private readonly BlobContainerClient _containerClient;
        private readonly string _accountName;
        private readonly string _containerName;

        public CatalogBlobStorage(IOptions<CatalogBlobStorageSettings> setttings)
        {
            string connectionString = setttings.Value.ConnectionString;
            _containerName = setttings.Value.ContainerName;
            _accountName = setttings.Value.AccountName;
            _containerClient = new BlobContainerClient(connectionString, _containerName);
        }

        public async Task<string> UploadImageToAzureBlobAsync(string base64Image)
        {
            try
            {
                string base64String = base64Image[(base64Image.IndexOf(',') + 1)..];
                string fileExtension = GetFileExtension(base64String);
                string blobName = Guid.NewGuid().ToString() + "." + fileExtension;

                // Convert the base64 string into a byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                BlobClient blobClient = _containerClient.GetBlobClient(blobName);

                // Upload the byte array to your blob
                using (var stream = new MemoryStream(imageBytes))
                {
                    await blobClient.UploadAsync(stream, overwrite: true);
                }

                return blobName;
            }
            catch
            {
                throw;
            }
        }

        public string GetImageURL(string blobName)
        {
            return $"https://{_accountName}.blob.core.windows.net/{_containerName}/{blobName}";
        }

        private static string GetFileExtension(string base64String)
        {
            var data = base64String[..5];

            return data.ToUpper() switch
            {
                "IVBOR" => "png",
                "/9J/4" => "jpg",
                "AAAAF" => "mp4",
                "JVBER" => "pdf",
                "AAABA" => "ico",
                "UMFYI" => "rar",
                "E1XYD" => "rtf",
                "U1PKC" => "txt",
                "MQOWM" or "77U/M" => "srt",
                _ => string.Empty,
            };
        }
    }
}
