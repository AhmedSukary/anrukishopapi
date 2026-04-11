using AnrukiShop_Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Exceptions;
using System.Security.Claims;

namespace AnrukiShop_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _service;

        public CheckoutController(ICheckoutService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                return Ok(new { OrderId = _service.Checkout(userId) });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }
    }
}