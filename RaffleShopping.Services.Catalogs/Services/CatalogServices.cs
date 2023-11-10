using Azure.Messaging.ServiceBus;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;
using RaffleShopping.Services.Catalogs.Repositories;

namespace RaffleShopping.Services.Catalogs.Services
{
    public class CatalogServices : ICatalogServices
    {
        private readonly ICatalogRepository _catalogRepository;
        public CatalogServices(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public void AddCatalog(AddCatalogDto addCatalogDto)
        {
            Catalog catalog = new Catalog
            {
                Title = addCatalogDto.Title,
                Description = addCatalogDto.Description,
                Price = addCatalogDto.Price
            };
            _catalogRepository.AddCatalogAsync(catalog);
        }
    }
}
