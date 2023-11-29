using RaffleShopping.Services.Customers.Models;

namespace RaffleShopping.Services.Customers.Repositories
{
    public interface ICustomerRepository
    {
        Task AddUserAsync(Customer customer);
        Task<Customer> GetUserByEmailAsync(string email);
    }
}