using ECommerceApp.API.Attributes;
using ECommerceApp.Application.DTOs;
using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [RedisCache(90)]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams specParams)
            => Ok(await _productService.GetAllProductsAsync(specParams));

        [HttpGet("{id}")]
        [RedisCache(90)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpGet("brands")]
        [RedisCache(300)]
        public async Task<IActionResult> GetAllBrands()
            => Ok(await _productService.GetAllBrandsAsync());

        [HttpGet("types")]
        [RedisCache(300)]
        public async Task<IActionResult> GetAllTypes()
            => Ok(await _productService.GetAllTypesAsync());
    }
}
