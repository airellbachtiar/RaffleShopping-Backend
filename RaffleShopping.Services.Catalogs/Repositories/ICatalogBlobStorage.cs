namespace RaffleShopping.Services.Catalogs.Repositories
{
    public interface ICatalogBlobStorage
    {
        string GetImageURL(string blobName);
        Task<string> UploadImageToAzureBlobAsync(string base64Image);
    }
}