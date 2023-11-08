using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace RaffleShopping.Services.Catalogs.Services
{
    public class CatalogServices
    {

        public CatalogServices() { }

        public async Task QueueCatalogAsync()
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
