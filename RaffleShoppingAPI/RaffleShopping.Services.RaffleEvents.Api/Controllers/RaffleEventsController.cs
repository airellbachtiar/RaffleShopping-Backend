using Microsoft.AspNetCore.Mvc;
using RaffleShoppping.Services.RaffleEvents.Dtos;

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
    }
}
