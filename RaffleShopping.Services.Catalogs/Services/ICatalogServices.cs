using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;

namespace RaffleShopping.Services.Catalogs.Services
{
    public interface ICatalogServices
    {
        Task AddCatalogAsync(AddCatalogDto addCatalogDto);
        Task<List<GetCatalogDto>> GetAllCatalogsAsync();
    }
}