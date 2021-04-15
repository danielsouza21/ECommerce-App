using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(id);
        }
    }
}
