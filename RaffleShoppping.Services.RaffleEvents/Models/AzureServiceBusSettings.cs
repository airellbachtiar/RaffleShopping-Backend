using Azure.Messaging.ServiceBus;

namespace RaffleShoppping.Services.RaffleEvents.Models
{
    public class AzureServiceBusSettings
    {
        public ServiceBusClient ServiceBusClient { get; set; } = new ServiceBusClient("");
        public string QueueName { get; set; } = "";
    }
}
