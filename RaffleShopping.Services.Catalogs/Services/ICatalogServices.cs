using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;

namespace RaffleShopping.Services.Catalogs.Services
{
    public interface ICatalogServices
    {
        void AddCatalog(AddCatalogDto addCatalogDto);
        List<GetCatalogDto> GetAllCatalogs();
    }
}