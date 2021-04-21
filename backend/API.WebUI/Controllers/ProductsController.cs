using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Interfaces;
using API.Infrastructure.Data.EfCore.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.WebUI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _repoProducts;
        private readonly IGenericRepository<ProductBrand> _repoBrands;
        private readonly IGenericRepository<ProductType> _repoTypes;

        public ProductsController(
                IGenericRepository<Product> repoProducts,
                IGenericRepository<ProductBrand> repoBrands,
                IGenericRepository<ProductType> repoTypes
            )
        {
            _repoProducts = repoProducts;
            _repoBrands = repoBrands;
            _repoTypes = repoTypes;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var spec = new ProductsWithTypesBrandsSpecification();

            var products = await _repoProducts.GetWithSpecAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var spec = new ProductsWithTypesBrandsSpecification(id);

            var product = await _repoProducts.GetEntityWithSpec(spec);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrandsAsync()
        {
            var productsBrands = await _repoBrands.GetAllAsync();
            return Ok(productsBrands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypesAsync()
        {
            var productsTypes = await _repoTypes.GetAllAsync();
            return Ok(productsTypes);
        }
    }
}
