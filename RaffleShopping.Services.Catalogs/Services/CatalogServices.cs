using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;
using RaffleShopping.Services.Catalogs.Repositories;

namespace RaffleShopping.Services.Catalogs.Services
{
    public class CatalogServices : ICatalogServices
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly ICatalogBlobStorage _catalogBlobStorage;
        public CatalogServices(ICatalogRepository catalogRepository, ICatalogBlobStorage catalogBlobStorage)
        {
            _catalogRepository = catalogRepository;
            _catalogBlobStorage = catalogBlobStorage;
        }

        public Task AddCatalogAsync(AddCatalogDto addCatalogDto)
        {
            string blobName = _catalogBlobStorage.UploadImageToAzureBlobAsync(addCatalogDto.Picture).Result;

            Catalog catalog = new Catalog
            {
                Title = addCatalogDto.Title,
                Description = addCatalogDto.Description,
                Price = addCatalogDto.Price,
                Picture = blobName
            };

            _catalogRepository.AddCatalogAsync(catalog);
        }

        public List<GetCatalogDto> GetAllCatalogs()
        {
            List<Catalog> catalogs = _catalogRepository.GetAllCatalogsAsync().Result;

            List<GetCatalogDto> catalogDtos = catalogs.Select(catalog => new GetCatalogDto
            {
                Title = catalog.Title,
                _id = catalog._id,
                Price = catalog.Price,
                PictureUrl = _catalogBlobStorage.GetImageURL(catalog.Picture)
            }).ToList();

            return catalogDtos;
        }
    }
}
