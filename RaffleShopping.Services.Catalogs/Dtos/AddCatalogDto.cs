using RaffleShopping.Services.Catalogs.Enums;

namespace RaffleShopping.Services.Catalogs.Dtos
{
    public class AddCatalogDto
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; }
        public string Picture { get; set; } = "";
        public EventType EventType { get; } = EventType.ADD_CATALOG;
    }
}
