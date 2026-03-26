using InventorySystem.API.Responce;
using InventorySystem.Application.DTOs;
using InventorySystem.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var res = await _auth.RegisterAsync(dto);
                if (!res.IsSuccess)
                    return BadRequest(new ApiResponse(500, res.Message));

                return Ok(new ApiResponse(200, res.Message, res));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Unexpected error", details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var res = await _auth.LoginAsync(dto);
            //if (res == null) return Unauthorized();

            //return Ok(res);


            if (!res.IsSuccess)
            {

                if (res.Message.Contains("Invalid") || res.Message.Contains("incorrect"))
                    return Unauthorized(new ApiResponse(401, res.Message));


                if (res.Message.Contains("locked"))
                    return StatusCode(403, new ApiResponse(403, res.Message));

                return BadRequest(new ApiResponse(400, res.Message));
            }

            return Ok(new ApiResponse(200, res.Message, res));
        }
    }
}
