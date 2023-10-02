using Microsoft.AspNetCore.Mvc;

namespace RaffleShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : Controller
    {
        private static readonly string[] ItemNames = new[]
        {
        "Nippers", "Ruler", "Eraser", "Sand Paper", "Cotton Buds", "Shaver", "Desk Lamp", "Speaker", "Pencil", "Deodorant"
        };

        private readonly ILogger<ItemController> _logger;

        public ItemController(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-items", Name = "GetItems")]
        public IEnumerable<ItemDTO> GetItems()
        {
            return Enumerable.Range(1, 5).Select(index => new ItemDTO
            {
                Name = ItemNames[Random.Shared.Next(ItemNames.Length)]
            })
            .ToArray();
        }

        [HttpGet("get-item", Name = "GetItem")]
        public ItemDTO GetItem()
        {
            return new ItemDTO
            {
                Name = ItemNames[Random.Shared.Next(ItemNames.Length)],
                Price = 0
            };
        }
    }
}
