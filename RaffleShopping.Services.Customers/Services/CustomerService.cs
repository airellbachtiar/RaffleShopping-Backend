using FirebaseAdmin.Auth;
using Microsoft.Azure.Cosmos;
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

        public async Task<bool> LoginAsync(LoginModel loginModel)
        {
            Customer customer = await  _customerRepository.GetUserByEmailAsync(loginModel.Email);

            if (customer != null) return true;
            return false;
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _customerRepository.GetUserByEmailAsync(email);
        }

        public async Task RegisterCustomerAsync(Customer customer)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = customer.Email,
                Password = customer.Password
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

            if(userRecord != null)
            {
                customer._id = userRecord.Uid;
                await _customerRepository.AddUserAsync(customer);
                var claims = new Dictionary<string, object>()
                {
                    { "role", customer.Role },
                };
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
            }
        }
    }
}
