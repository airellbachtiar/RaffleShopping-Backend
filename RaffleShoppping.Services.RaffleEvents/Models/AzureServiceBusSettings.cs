using Azure.Messaging.ServiceBus;

namespace RaffleShoppping.Services.RaffleEvents.Models
{
    public class AzureServiceBusSettings
    {
        public ServiceBusClient ServiceBusClient { get; set; }
        public string QueueName { get; set; }
    }
}
