using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace AnrukiShop_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {         
            try
            {            
                return Ok(_service.GetAllUsers());
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpGet("GetBy/{id:int}")]
        public async Task<ActionResult> GetById(int id, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid ID");

            try
            {
                var authResult = await authorizationService.AuthorizeAsync(User, id, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                return Ok(_service.GetById(id));
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [AllowAnonymous]
        [EnableRateLimiting("AuthLimiter")]
        [HttpPost("SendEmailVerificationCode")]
        public async Task<ActionResult> SendEmailVerificationCode([FromBody] SendEmailCodeRequest request)
        {
            try
            {
                await _service.SendEmailVerificationCode(request.Email);

                return Ok(new { success = "Verification code sendet" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [AllowAnonymous]
        [EnableRateLimiting("AuthLimiter")]
        [HttpPost("CheckEmailVerificationCode")]
        public IActionResult CheckEmailVerificationCode([FromBody] CheckEmailCodeRequest request)
        {
            if (_service.CheckEmailVerificationCode(request.Code))
            {
                _service.DeleteEmailVerificationCode(request.Code);
                return Ok(new { success = "Verification code is match" });
            }
            return BadRequest(new { Code = "ERROR", Message = "Verification code not match" });
        }

        [AllowAnonymous]
        [EnableRateLimiting("AuthLimiter")]
        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateUserRequest request)
        {
            try
            {
                UserModel model = new()
                {
                    Email = request.Email,
                    FullName = request.Name,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber,
                    Gender = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                };

                int newId = _service.Create(model);

                return Ok(new { ID = newId });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("UpdateProfile/{id:int}")]
        public async Task<ActionResult> UpdateProfile(int id, [FromBody] UpdateProfileRequest request, [FromServices] IAuthorizationService authorizationService)
        {
            if (id < 1)
                return BadRequest("Invalid ID");

            try
            {
                var authResult = await authorizationService.AuthorizeAsync(User, id, "OwnershipPolicy");

                if (!authResult.Succeeded)
                    return Forbid();

                _service.UpdateProfile(id, request.FullName, request.PhoneNumber, request.Gender, request.DateOfBirth);
                return Ok(new { success = "user updated successfully" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("ChangePassword")]
        public IActionResult ChangeEmailAndPassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                _service.ChangePassword(userId, request.NewPassword);

                return Ok(new { success = "password is changed successfully" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("ChangeRole/{id:int}")]
        public IActionResult ChangeRole(int id, [FromBody] ChangeRoleRequest request)
        {
            try
            {
                _service.ChangeRole(id, request.Role);
                return Ok(new { success = "user roel changed successfully" });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok(new { success = "user deleted successfully" });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
