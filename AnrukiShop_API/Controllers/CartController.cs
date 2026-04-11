using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;
using AnrukiShop_Application.Models;

namespace AnrukiShop_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetBy/{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
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
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult> AddItem([FromBody] AddToCartRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var cart = _service.GetById(request.CartId);

                var authResult = await authorizationService.AuthorizeAsync(User, cart.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                CartItemModel model = new()
                {
                    CartId = request.CartId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                 _service.AddItem(model);

                return Ok(new { success = "the item added to cart" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("UpdateItemQuantity")]
        public async Task<ActionResult> UpdateItemQuantity(UpdateCartItemQuantityRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var cart = _service.GetById(request.CartId);

                var authResult = await authorizationService.AuthorizeAsync(User, cart.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.UpdateItemQuantity(request.ItemId, request.Quantity);
                return Ok(new { success = "quantity updated" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpDelete("RemoveItem")]
        public async Task<ActionResult> RemoveItem(int cartId, int itemId, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var cart = _service.GetById(cartId);

                var authResult = await authorizationService.AuthorizeAsync(User, cart.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.RemoveItem(itemId);

                return Ok(new { succees = "cart item deleted" });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}