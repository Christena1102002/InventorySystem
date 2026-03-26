using InventorySystem.API.Responce;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService saleService;
        public SalesController(ISaleService _saleService)
        {
            saleService = _saleService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSale(CreateSaleDTO dTO)
        {
            var result=await saleService.CreateSaleAsync(dTO);
            if (!result.IsSuccess)
                return BadRequest(new ApiResponse(400, result.Message, result.Errors));

            return Ok(new ApiResponse(200, result.Message, result.Data));
        }
    }
}
