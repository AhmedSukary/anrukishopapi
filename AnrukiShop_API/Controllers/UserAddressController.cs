using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;

namespace AnrukiShop_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserAddressController : Controller
    {
        private readonly IUserAddressService _service;

        public UserAddressController(IUserAddressService service)
        {
            _service = service;
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult> GetById(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid ID");

            try
            {
                var model = _service.GetById(id);

                var authResult = await authorizationService.AuthorizeAsync(User, model.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                return Ok(model);
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpGet("GetDefaultAddressByUserId/{id:int}")]
        public async Task<ActionResult> GetDefaultAddressByUserId(int id, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(User, id, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                var model = _service.GetDefaultAddressByUserId(id);
                return Ok(model);
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpGet("GetByUser/{id:int}")]
        public async Task<ActionResult> GetByUser(int id, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(User, id, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                return Ok(_service.GetByUser(id));
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] CreateUserAddressRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var authResult = await authorizationService.AuthorizeAsync(User, request.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                UserAddressModel model = new()
                {
                    UserId = request.UserId,
                    Country = request.Country,
                    City = request.City,
                    Region = request.Region,
                    AddressLine = request.AddressLine,
                    IsDefault = request.IsDefault
                };

                int newId = _service.Create(model);

                return Ok(_service.GetById(newId));
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UpdateUserAddressRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var model = _service.GetById(request.Id);

                var authResult = await authorizationService.AuthorizeAsync(User, model.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.Update(request.Id, request.Country, request.City, request.Region, request.AddressLine);
                return Ok(new {success = "user address updated successfully"});
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpDelete("{id:int}/Delete")]
        public async Task<ActionResult> Delete(int id, [FromServices] IAuthorizationService authorizationService)
        {
            try
            {
                var model = _service.GetById(id);

                var authResult = await authorizationService.AuthorizeAsync(User, model.UserId, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.Delete(id);
                return Ok(new { success = "user address deleted successfully" });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
