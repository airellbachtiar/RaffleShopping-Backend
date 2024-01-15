using RaffleShopping.Services.Catalogs.Enums;
using System.ComponentModel.DataAnnotations;

namespace RaffleShopping.Services.Catalogs.Dtos
{
    public class AddCatalogDto
    {
        [StringLength(40, ErrorMessage = "The input must be at most 40 characters long.")]
        public string Title { get; set; } = "";

        [StringLength(200, ErrorMessage = "The input must be at most 200 characters long.")]
        public string Description { get; set; } = "";

        public double Price { get; set; }

        public string Picture { get; set; } = "";

        public EventType EventType { get; } = EventType.ADD_CATALOG;
    }
}
