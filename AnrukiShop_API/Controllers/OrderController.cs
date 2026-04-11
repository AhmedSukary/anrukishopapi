using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace AnrukiShop_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult> GetById(int id, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var model = _service.GetById(id);
                var authResult = await authorizationService.AuthorizeAsync(User, model.UserId, "OwnershipPolicy");

                return Ok(model);
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("GetByUser/{id:int}")]
        public async Task<ActionResult> GetByUserId(int id, [FromServices] IAuthorizationService authorizationService)
        {
            var authResult = await authorizationService.AuthorizeAsync(User, id, "OwnershipPolicy");

            if (!authResult.Succeeded)
                return Forbid();

            return Ok(_service.GetByUserId(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("MarkAsShipped/{id:int}")]
        public IActionResult MarkAsShipped(int id)
        {
            try
            {
                _service.MarkAsShipped(id);
                return Ok(new { success = "order status updated" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("MarkAsCancelled/{id:int}")]
        public async Task<ActionResult> MarkAsCancelled(int id, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var model = _service.GetById(id);
                var authResult = await authorizationService.AuthorizeAsync(User, model.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.MarkAsCancelled(id);

                return Ok(new { success = "order status updated" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);

                return Ok(new { success = "order deleted" });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
