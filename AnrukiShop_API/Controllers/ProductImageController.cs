using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;

namespace AnrukiShop_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _service;

        public ProductImageController(IProductImageService service)
        {
            _service = service;
        }

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

        [AllowAnonymous]
        [HttpGet("GetProductImagesById/{id:int}")]
        public IActionResult GetByProductId(int id)
        {
            return Ok(_service.GetProductImagesById(id));
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateProductImageRequest request)
        {
            try
            {
                ProductImageModel model = new()
                {
                    ProductId = request.ProductId,
                    Url = request.Url,
                    IsPrimary = request.IsPrimary,
                };

                int newId = _service.Create(model);

                model = _service.GetById(newId);

                return Ok(model);
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("Update/{id:int}")]
        public IActionResult Update(int id, [FromBody] UpdateProductImageRequest request)
        {
            try
            {
                _service.Update(id, request.Url, request.IsPrimary);

                return Ok("product image updated successfully");
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpDelete("{id:int}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("product image deleted successfully");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}