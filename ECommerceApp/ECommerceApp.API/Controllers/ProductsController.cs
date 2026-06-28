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

        // GET /api/products
        // GET /api/products?brandId=2&typeId=1&search=jacket&sort=priceDesc&pageIndex=1&pageSize=6
        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams specParams)
        {
            var result = await _productService.GetAllProductsAsync(specParams);
            return Ok(result);
        }

        // GET /api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET /api/products/brands
        [HttpGet("brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }

        // GET /api/products/types
        [HttpGet("types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
