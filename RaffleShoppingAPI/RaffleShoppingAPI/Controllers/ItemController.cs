using Microsoft.AspNetCore.Mvc;

namespace RaffleShoppingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetItems")]
        public IEnumerable<ItemDTO> GetItems()
        {
            return Enumerable.Range(1, 5).Select(index => new ItemDTO
            {
                Name = $"{Random.Shared.Next(Summaries.Length)}"
            })
            .ToArray();
        }

        [HttpGet]
        public ItemDTO Get()
        {
            return new ItemDTO
            {
                Name = $"{Random.Shared.Next(Summaries.Length)}"
            };
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
