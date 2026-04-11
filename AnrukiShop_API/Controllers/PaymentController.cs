using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using AnrukiShop_API.Requests;
using System.Security.Claims;

namespace AnrukiShop_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Create")]
        public IActionResult Craete([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                return Ok(new { PaymentId = _paymentService.Create(userId, request.OrderId, request.Method) });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPost("Pay")]
        public IActionResult Pay([FromBody] PayRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                _paymentService.Pay(userId, request.PaymentId, request.TransactionRef);
                return Ok(new { success = "order paid successfully Thenk you for shopping" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetBy/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_paymentService.GetById(id));
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByOrder/{id:int}")]
        public IActionResult GetByOrderId(int id)
        {
            try
            {
                return Ok(_paymentService.GetByOrderId(id));
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }
    }
}