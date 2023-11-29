using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace RaffleShopping.Services.Customers.Models
{
    public class Customer
    {
        [BsonId]
        public string _id { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}
