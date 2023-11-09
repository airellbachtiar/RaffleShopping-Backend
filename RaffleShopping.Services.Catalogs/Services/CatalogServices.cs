using Azure.Identity;
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
            _ = QueueCatalogAsync();
        }

        private async Task QueueCatalogAsync()
        {
            ServiceBusClient client;

            ServiceBusSender sender;

            const int numOfMessages = 3;

            var clientOptions = new ServiceBusClientOptions
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            client = new ServiceBusClient(
                "raffleshopping.servicebus.windows.net",
                new DefaultAzureCredential(),
                clientOptions);
            sender = client.CreateSender("catalogs");

            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

            for (int i = 1; i <= numOfMessages; i++)
            {
                if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
                {
                    throw new Exception($"The message {i} is too large to fit in the batch.");
                }
            }

            try
            {
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }

    }
}
