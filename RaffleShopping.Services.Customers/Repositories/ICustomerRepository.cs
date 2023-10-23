using RaffleShopping.Services.Customers.Models;

namespace RaffleShopping.Services.Customers.Repositories
{
    public interface ICustomerRepository
    {
        void AddUserAsync(Customer customer);
        Customer GetUserByEmailAsync(string email);
    }
}