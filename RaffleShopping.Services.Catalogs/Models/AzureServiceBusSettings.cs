using Azure.Messaging.ServiceBus;

namespace RaffleShopping.Services.Catalogs.Models
{
    public class AzureServiceBusSettings
    {
        public ServiceBusClient ServiceBusClient { get; set; } = new ServiceBusClient("");
        public string QueueName { get; set; } = "";
    }
}
