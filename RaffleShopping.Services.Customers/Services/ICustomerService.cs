using RaffleShopping.Services.Customers.Models;

namespace RaffleShopping.Services.Customers.Services
{
    public interface ICustomerService
    {
        Customer GetCustomerByEmail(string email);
        bool Login(LoginModel loginModel);
        void RegisterCustomer(Customer customer);
    }
}