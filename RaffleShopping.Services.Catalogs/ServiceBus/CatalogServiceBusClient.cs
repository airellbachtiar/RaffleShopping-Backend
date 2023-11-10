using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Models;
using System.Text;
using System.Text.Json;

namespace RaffleShopping.Services.Catalogs.ServiceBus
{
    public class CatalogServiceBusClient : ICatalogServiceBusClient
    {
        private readonly Azure.Messaging.ServiceBus.ServiceBusSender _serviceBusSender;
        public CatalogServiceBusClient(IOptions<AzureServiceBusSettings> azureServiceBusSettings)
        {
            _serviceBusSender = azureServiceBusSettings.Value.ServiceBusClient.CreateSender(azureServiceBusSettings.Value.QueueName);
        }

        public void AddCatalog(AddCatalogDto addCatalogDto)
        {
            string messageBody = JsonSerializer.Serialize(addCatalogDto);
            QueueMessage(messageBody);
        }

        private async void QueueMessage(string messageBody)
        {
            ServiceBusMessage message = new ServiceBusMessage(Encoding.UTF8.GetBytes(messageBody));
            await _serviceBusSender.SendMessageAsync(message);
        }
    }
}
