using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RaffleShopping.Services.Customers.Models
{
    public class Customer
    {
        [BsonId]
        #pragma warning disable IDE1006 // Naming Styles
        public string _id { get; set; } = "";
        #pragma warning restore IDE1006 // Naming Styles

        [Required]
        public string Email { get; set; } = "";

        public string Role { get; set; } = "";
    }
}
