using RaffleShopping.Services.Catalogs.Dtos;

namespace RaffleShopping.Services.Catalogs.ServiceBus
{
    public interface ICatalogServiceBusClient
    {
        Task AddCatalogAsync(AddCatalogDto addCatalogDto);
    }
}