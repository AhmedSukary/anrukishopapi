using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using AnrukiShop_Application.Models;
using AnrukiShop_Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AnrukiShop_API.Requests;

namespace AnrukiShop_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewController : Controller
    {
        private readonly IProductReviewService _service;

        public ProductReviewController(IProductReviewService service)
        {
            _service = service;
        }

        [AllowAnonymous]
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
        [HttpGet("GetByProduct/{id:int}")]
        public IActionResult GetByProduct(int id)
        {
            return Ok(_service.GetByProductId(id));
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateProductReviewRequest request)
        {
            try
            {
                ProductReviewModel model = new()
                {
                    ProductId = request.ProductId,
                    UserName = request.UserName,
                    Rating = request.Rating,
                    Comment = request.Comment
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
        public IActionResult Update(int id, UpdateProductReviewRequest request)
        {
            try
            {
                _service.Update(id, request.Rating, request.Comment);
                return Ok("review updated successfully");
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
                return Ok("review deleted successfully");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
