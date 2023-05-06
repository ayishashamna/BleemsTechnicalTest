using ProductsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : Controller
    {
        private readonly ProductServices _services;
        public ProductsApiController(ProductServices services)
        {
            _services = services;
        }
        [HttpGet]
        [Route("GetProductsListRandomOrder")]
        public async Task<IActionResult> GetProductsListRandomOrder()
        {
            return Ok(await _services.GetProductsInfo());
        }
        [HttpGet]
        [Route("GetProductDetailsById/{productId}")]
        public async Task<IActionResult> GetProductDetailsById(int productId)
        {
            return Ok(await _services.GetProductById(productId));
        }
        [HttpGet]
        [Route("GetProductDetailsByIdWithCurrency/{productId}")]
        public async Task<IActionResult> GetProductDetailsByIdWithCurrency(int productId)
        {
            return Ok(await _services.GetProductDetailsByIdWithCurrency(productId));
        }
    }
}
