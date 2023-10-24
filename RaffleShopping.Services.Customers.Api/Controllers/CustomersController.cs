using Microsoft.AspNetCore.Mvc;
using RaffleShopping.Services.Customers.Dtos;
using RaffleShopping.Services.Customers.Features.HashingString;
using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Services;

namespace RaffleShopping.Services.Customers.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService) 
        {
            _customerService = customerService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (_customerService.Login(loginModel))
            {
                // Authentication successful
                return Ok(new { Message = "Login successful" });
            }
            else
            {
                // Authentication failed
                return Unauthorized(new { Message = "Invalid credentials" });
            }

        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> RegisterUser([FromBody] SignUpCustomerDto customerDto)
        {
            try
            {
                Customer existingUser = _customerService.GetCustomerByEmail(customerDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists");
                }

                Customer customer = new Customer()
                {
                    Email = customerDto.Email,
                    Password = HashString.Hash(customerDto.Password)
                };

                _customerService.RegisterCustomer(customer);

                return Ok("User registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
