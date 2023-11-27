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
                _catalogServices.AddCatalog(addCatalogDto);
                _serviceBusClient.AddCatalog(addCatalogDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public List<GetCatalogDto> GetAllCatalogs()
        {
            try
            {
                return _catalogServices.GetAllCatalogs();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
