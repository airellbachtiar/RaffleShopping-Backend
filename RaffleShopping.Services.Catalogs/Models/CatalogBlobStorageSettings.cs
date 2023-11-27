namespace RaffleShopping.Services.Catalogs.Models
{
    public class CatalogBlobStorageSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string ContainerName { get; set; } = null!;

        public string AccountName { get; set; } = null!;
    }
}
