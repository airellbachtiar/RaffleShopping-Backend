using Microsoft.AspNetCore.Mvc;
using RaffleShopping.Services.Catalogs.Dtos;
using RaffleShopping.Services.Catalogs.Services;

namespace RaffleShopping.Services.Catalogs.Api.Controllers
{
    [Route("api/catalogs")]
    [ApiController]
    public class CatalogsController : Controller
    {
        private readonly ICatalogServices _catalogServices;
        public CatalogsController(ICatalogServices catalogServices)
        {
            _catalogServices = catalogServices;
        }

        [HttpPost]
        public IActionResult AddCatalog([FromBody] AddCatalogDto addCatalogDto)
        {
            try
            {
                _catalogServices.AddCatalog(addCatalogDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
