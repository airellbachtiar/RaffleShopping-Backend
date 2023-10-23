using RaffleShopping.Services.Customers.Features.HashingString;
using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;

namespace RaffleShopping.Services.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool Login(LoginModel loginModel)
        {
            Customer customer = _customerRepository.GetUserByEmailAsync(loginModel.Email);
            string password = HashString.Hash(loginModel.Password);

            if (customer != null && password == customer.Password) return true;
            return false;
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetUserByEmailAsync(email);
        }

        public void RegisterCustomer(Customer customer)
        {
            _customerRepository.AddUserAsync(customer);
        }
    }
}
