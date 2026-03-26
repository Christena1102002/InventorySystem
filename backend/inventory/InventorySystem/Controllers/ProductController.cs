using InventorySystem.API.Responce;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using InventorySystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }
        [HttpPost("Create Product")]
        [SwaggerOperation(Summary = "Create Product")]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dTO)
        {
            var result = await productService.CreateProductAsync(dTO);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(400, result.Message, result.Errors));

            return Ok(new ApiResponse(200, result.Message,result.Data));
        }
        [SwaggerOperation(Summary = "Get All Products")]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await productService.GetAllProductsAsync();
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(400, result.Message, result.Errors));

            return Ok(new ApiResponse(200, result.Message, result.Data));
        }

        [HttpGet("Pageination & Search (***bouns***)")]
        public async Task<IActionResult> GetAllbypagenation(int pageNumber = 1, int pageSize = 10, string? search = null)
        {
            var result = await productService.GetProductsPagedAsync(pageNumber, pageSize,search);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(400, result.Message, result.Errors));

            return Ok(new ApiResponse(200, result.Message, result.Data));
        }
    }
}
