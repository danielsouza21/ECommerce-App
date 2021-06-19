using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Interfaces;
using API.Core.Specifications;
using API.Services.DTOs;
using API.Services.ErrorHandlers;
using API.Services.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.WebUI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IStoreServices _storeServices;
        private readonly IMapper _mapper;

        public ProductsController(IStoreServices storeServices, IMapper mapper)
        {
            _storeServices = storeServices;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Pagination<ProductToReturnDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductsAsync([FromQuery]ProductSpecParams productParams)
        {
            var products = await _storeServices.GetProductsAsync(productParams);
            var totalItems = await _storeServices.GetCountAsync(productParams);

            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);  //list<product> to list<DTO product>
            var productPagination = new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems, productsDto);

            return Ok(productPagination);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _storeServices.GetProductById(id);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var productDto = _mapper.Map<Product, ProductToReturnDto>(product);

            return Ok(productDto);
        }

        [HttpGet("brands")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductBrand>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductBrandsAsync()
        {
            var productBrands = await _storeServices.GetAllProductsBrands();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductType>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProductTypesAsync()
        {
            var productsTypes = await _storeServices.GetAllProductsTypes();
            return Ok(productsTypes);
        }
    }
}
