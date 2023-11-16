using Azure.Messaging.ServiceBus;

namespace RaffleShopping.Services.Catalogs.Models
{
    public class AzureServiceBusSettings
    {
        public ServiceBusClient ServiceBusClient { get; set; }
        public string QueueName { get; set; }
    }
}
