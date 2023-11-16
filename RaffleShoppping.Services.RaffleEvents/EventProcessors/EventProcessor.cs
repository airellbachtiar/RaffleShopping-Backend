using Microsoft.Extensions.DependencyInjection;
using RaffleShoppping.Services.RaffleEvents.Dtos;
using RaffleShoppping.Services.RaffleEvents.Enums;
using System.Text.Json;

namespace RaffleShoppping.Services.RaffleEvents.EventProcessors
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public EventProcessor(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void ProcessEvent(string message)
        {
            switch (GetEventType(message))
            {
                case EventType.ADD_CATALOG:
                    AddCatalog(message);
                    break;
                default: break;
            }
        }

        private EventType GetEventType(string message)
        {
            var eventType = JsonSerializer.Deserialize<MessageEventDto>(message);
            if (eventType != null) return eventType.EventType;
            return EventType.UNDERTIMINED;
        }

        private void AddCatalog(string message)
        {
            using (var scoper = _serviceScopeFactory.CreateScope())
            {
                var catalog = JsonSerializer.Deserialize<AddCatalogDto>(message);
                Console.WriteLine(catalog);
            }
        }
    }
}
