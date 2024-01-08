using Moq;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;
using RaffleShopping.Services.Catalogs.Repositories;
using RaffleShopping.Services.Catalogs.Services;

namespace RaffleShopping.Services.Catalogs.UnitTests
{
    public class CatalogServiceTests
    {
        private CatalogServices _catalogServices;
        private Mock<ICatalogRepository> _catalogRepositoryMock;
        private Mock<ICatalogBlobStorage> _blobStorageMock;

        [SetUp]
        public void Setup()
        {
            // Create a mock object for the catalog repository
            _catalogRepositoryMock = new Mock<ICatalogRepository>();
            _blobStorageMock = new Mock<ICatalogBlobStorage>();

            // Create an instance of the CatalogServices class
            _catalogServices = new CatalogServices(_catalogRepositoryMock.Object, _blobStorageMock.Object);
        }

        [Test]
        public async Task AddCatalog_ValidDto_CallsAddCatalogAsync()
        {
            // Arrange
            var addCatalogDto = new AddCatalogDto
            {
                Title = "Test Catalog",
                Description = "Test Description",
                Price = 10.99
            };

            // Act
            await _catalogServices.AddCatalogAsync(addCatalogDto);

            // Assert
            _catalogRepositoryMock.Verify(x => x.AddCatalogAsync(It.IsAny<Catalog>()), Times.Once);
        }
    }
}
