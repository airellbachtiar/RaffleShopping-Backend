using RaffleShopping.Services.Customers.Dtos;
using RaffleShopping.Services.Customers.Models;

namespace RaffleShopping.Services.Customers.Services
{
    public interface ICustomerService
    {
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<bool> LoginAsync(LoginModel loginModel);
        Task RegisterCustomerAsync(SignUpCustomerDto customerDto);
        Task DeleteCustomerAsync(string customerId);
    }
}