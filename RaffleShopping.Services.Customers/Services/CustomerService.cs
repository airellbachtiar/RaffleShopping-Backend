using FirebaseAdmin.Auth;
using Microsoft.Azure.Cosmos;
using RaffleShopping.Services.Customers.Dtos;
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

        public async Task RegisterCustomerAsync(SignUpCustomerDto customerDto)
        {
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = customerDto.Email,
                Password = customerDto.Password
            };
            UserRecord userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);

            if(userRecord != null)
            {
                Customer customer = new Customer()
                {
                    _id = userRecord.Uid,
                    Email = userRecord.Email,
                    Role = customerDto.Role
                };
                await _customerRepository.AddUserAsync(customer);
                var claims = new Dictionary<string, object>()
                {
                    { "role", customer.Role },
                };
                await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(userRecord.Uid, claims);
            }
        }

        public async Task DeleteCustomerAsync(string customerId)
        {
            await FirebaseAuth.DefaultInstance.DeleteUserAsync(customerId);
            await _customerRepository.DeleteUserAsync(customerId);
        }
    }
}
