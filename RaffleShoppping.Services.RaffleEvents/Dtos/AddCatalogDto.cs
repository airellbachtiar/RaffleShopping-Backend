using RaffleShoppping.Services.RaffleEvents.Enums;

namespace RaffleShoppping.Services.RaffleEvents.Dtos
{
    public class AddCatalogDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public EventType EventType { get; } = EventType.ADD_CATALOG;
    }
}
