using System.Threading.Tasks;
using API.Core.Interfaces;
using API.WebUI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.WebUI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IStoreServices _storeServices;

        public ProductsController(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _storeServices.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _storeServices.GetProductById(id);

            var productDto = new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                ProductBrand = product.ProductBrand.Name,
                ProductType = product.ProductType.Name
            };

            return Ok(productDto);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrandsAsync()
        {
            var productBrands = await _storeServices.GetAllProductsBrands();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypesAsync()
        {
            var productsTypes = await _storeServices.GetAllProductsTypes();
            return Ok(productsTypes);
        }
    }
}
