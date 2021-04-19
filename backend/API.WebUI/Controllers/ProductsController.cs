using System.Threading.Tasks;
using API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.WebUI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repoProduct;

        public ProductsController(IProductRepository repoProduct)
        {
            _repoProduct = repoProduct;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _repoProduct.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _repoProduct.GetProductByIdAsync(id);
            return Ok(product);
        }
    }
}
