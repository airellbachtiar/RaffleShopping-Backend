using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RaffleShopping.Services.Catalogs.Models
{
    public class Catalog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        #pragma warning disable IDE1006 // Naming Styles
        public string _id { get; set; } = "";

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public double Price { get; set; }

        public string Picture { get; set; } = "";
    }
}
