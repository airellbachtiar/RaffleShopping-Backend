using RaffleShopping.Services.Catalogs.Models;

namespace RaffleShopping.Services.Catalogs.Repositories
{
    public interface ICatalogRepository
    {
        Task AddCatalogAsync(Catalog catalog);
        Task<List<Catalog>> GetAllCatalogsAsync();
    }
}