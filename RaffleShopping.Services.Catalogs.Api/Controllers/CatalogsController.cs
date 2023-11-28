using Microsoft.AspNetCore.Mvc;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.ServiceBus;
using RaffleShopping.Services.Catalogs.Services;

namespace RaffleShopping.Services.Catalogs.Api.Controllers
{
    [Route("api/catalogs")]
    [ApiController]
    public class CatalogsController : Controller
    {
        private readonly ICatalogServices _catalogServices;
        private readonly ICatalogServiceBusClient _serviceBusClient;

        public CatalogsController(ICatalogServices catalogServices, ICatalogServiceBusClient serviceBusClient)
        {
            _catalogServices = catalogServices;
            _serviceBusClient = serviceBusClient;
        }

        [HttpPost]
        public IActionResult AddCatalog([FromBody] AddCatalogDto addCatalogDto)
        {
            try
            {
                _catalogServices.AddCatalogAsync(addCatalogDto);
                _serviceBusClient.AddCatalogAsync(addCatalogDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetCatalogDto>>> GetAllCatalogsAsync()
        {
            try
            {
                return Ok(await _catalogServices.GetAllCatalogsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
