using Microsoft.AspNetCore.Mvc;
using RaffleShopping.Services.Customers.Models;

namespace RaffleShopping.Services.Customers.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class CustomersController : Controller
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Replace this with your actual authentication logic
            if (loginModel.Email == "example@email.com" && loginModel.Password == "examplePassword")
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
    }
}
