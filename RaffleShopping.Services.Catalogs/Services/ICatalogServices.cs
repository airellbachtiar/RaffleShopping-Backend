using RaffleShopping.Services.Catalogs.Dtos;

namespace RaffleShopping.Services.Catalogs.Services
{
    public interface ICatalogServices
    {
        void AddCatalog(AddCatalogDto addCatalogDto);
    }
}