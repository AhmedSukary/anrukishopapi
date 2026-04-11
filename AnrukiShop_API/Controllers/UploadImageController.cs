using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace AnrukiShop_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : Controller
    {
        private readonly IUploadImageService _service;

        public UploadImageController(IUploadImageService service)
        {
            _service = service;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new { success = false, message = "No file uploaded" });

                if (file.Length > 5 * 1024 * 1024)
                    return BadRequest(new { success = false, message = "Image too large" });

                var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };

                if (!allowedTypes.Contains(file.ContentType))
                    return BadRequest(new { success = false, message = "Invalid image type" });

                using var ms = new MemoryStream();

                await file.CopyToAsync(ms);

                var base64Image = Convert.ToBase64String(ms.ToArray());

                var url = await _service.UploadImage(base64Image);

                return Ok(new { success = true, imageUrl = url, });
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }
    }
}