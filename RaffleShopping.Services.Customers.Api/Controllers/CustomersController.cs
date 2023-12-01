using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "Public")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                if (await _customerService.LoginAsync(loginModel))
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> RegisterCustomer([FromBody] SignUpCustomerDto customerDto)
        {
            try
            {
                Customer existingUser = await _customerService.GetCustomerByEmailAsync(customerDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("User with this email already exists");
                }

                await _customerService.RegisterCustomerAsync(customerDto);

                return Ok("User registration successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "Public")]
        public async Task<IActionResult> DeleteCustomer()
        {
            try
            {
                await _customerService.DeleteCustomerAsync(User.FindFirst("uid")?.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
