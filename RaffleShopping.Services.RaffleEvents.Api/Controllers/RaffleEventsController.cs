using Microsoft.AspNetCore.Mvc;
using RaffleShoppping.Services.RaffleEvents.Dtos;
using RaffleShoppping.Services.RaffleEvents.Services;

namespace RaffleShopping.Services.RaffleEvents.Api.Controllers
{
    [ApiController]
    [Route("api/raffle-events")]
    public class RaffleEventsController : Controller
    {
        [HttpGet("get-raffle-event", Name = "GetRaffleEvent")]
        public List<RaffleEventDto> GetRaffleEvent()
        {
            return new List<RaffleEventDto> 
            {   
                new RaffleEventDto
                {
                    Id = 1,
                    Title = "Test Item 1",
                    Price = 0
                } ,
                new RaffleEventDto
                {
                    Id = 2,
                    Title = "Test Item 2",
                    Price = 3
                }
            };
        }

        [HttpGet]
        [Route("get-catalogs")]
        public IActionResult GetCatalogsFromMessageBus()
        {
            RaffleEventService catalogService = new RaffleEventService();
            _ = catalogService.ReceiveMessageAsync();
            return Ok();
        }
    }
}
