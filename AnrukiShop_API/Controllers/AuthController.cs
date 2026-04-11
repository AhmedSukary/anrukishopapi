using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using AnrukiShop_API.Auth;
using Microsoft.AspNetCore.RateLimiting;

namespace AnrukiShop_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;           
        }

        [EnableRateLimiting("AuthLimiter")]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var model = _service.Login(request.Email, request.Password, ip);

                return Ok(new TokenResponse
                {
                    UserId = model.UserId,
                    AccessToken = model.AccessToken,
                    RefreshToken = model.RefreshToken
                });
            }
            catch (AppException ex)
            {               
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [EnableRateLimiting("AuthLimiter")]
        [HttpPost("Refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                var model = _service.RefreshToken(request.RefreshToken);

                return Ok(new TokenResponse
                {
                    UserId = model.UserId,
                    AccessToken = model.AccessToken,
                    RefreshToken = model.RefreshToken
                });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpPost("Logout")]
        public IActionResult Login([FromBody] LogoutRequest request)
        {
            try
            {
               return Ok(_service.Logout(request.RefreshToken));           
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
