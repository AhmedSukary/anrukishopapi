using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;

namespace AnrukiShop_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _service;

        public WishlistController(IWishlistService service)
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

        [HttpPost("AddToWishlist")]
        public async Task<ActionResult> AddItem([FromBody] AddToWishlistRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var wishlist = _service.GetById(request.WishlistId);

                var authResult = await authorizationService.AuthorizeAsync(User, wishlist.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                int newId = _service.AddItem(request.WishlistId, request.ProductId);

                return Ok(new { Id = newId });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpDelete("RemoveItem")]
        public async Task<ActionResult> RemoveItem(int wishlistId, int productId, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var Wishlist = _service.GetById(wishlistId);

                var authResult = await authorizationService.AuthorizeAsync(User, Wishlist.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.RemoveItem(wishlistId, productId);

                return Ok(new { succees = "Wishlist item deleted" });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}