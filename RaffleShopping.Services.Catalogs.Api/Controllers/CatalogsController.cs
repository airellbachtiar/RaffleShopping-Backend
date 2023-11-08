using Microsoft.AspNetCore.Mvc;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Services;

namespace RaffleShopping.Services.Catalogs.Api.Controllers
{
    [Route("api/catalogs")]
    [ApiController]
    public class CatalogsController : Controller
    {
        public CatalogsController() { }

        [HttpPost("addCatalog")]
        public IActionResult AddCatalog([FromBody] AddCatalogDto addCatalogDto)
        {
            CatalogServices catalogServices = new CatalogServices();
            _ = catalogServices.QueueCatalogAsync();
            return Ok();
        }
    }
}
