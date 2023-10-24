using System.ComponentModel.DataAnnotations;

namespace RaffleShopping.Services.Customers.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public string _Id { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
