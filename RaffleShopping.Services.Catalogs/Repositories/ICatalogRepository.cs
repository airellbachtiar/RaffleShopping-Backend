using RaffleShopping.Services.Catalogs.Models;

namespace RaffleShopping.Services.Catalogs.Repositories
{
    public interface ICatalogRepository
    {
        void AddCatalogAsync(Catalog catalog);
    }
}