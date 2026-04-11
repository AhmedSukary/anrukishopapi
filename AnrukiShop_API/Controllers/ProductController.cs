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
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
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
        [HttpGet("GetSummaryBy/{id:int}")]
        public IActionResult GetProductSummary(int id)
        {
            try
            {
                return Ok(_service.GetSummaryById(id));
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("GetProductsSummary")]
        public IActionResult GetProductsSummary()
        {
            return Ok(_service.GetProductsSummary());
        }

        [AllowAnonymous]
        [HttpGet("GetProductsSummaryByCategory/{id:int}")]
        public IActionResult GetProductsSummaryByCategoryId(int id)
        {
            return Ok(_service.GetProductsSummaryByCategoryId(id));
        }

        [AllowAnonymous]
        [HttpGet("GetByCategory/{id:int}")]
        public IActionResult GetByCategory(int id)
        {
            return Ok(_service.GetByCategoryId(id));
        }

        [AllowAnonymous]
        [HttpGet("SearchProducts/{query}")]
        public IActionResult SearchProducts(string query)
        {
            return Ok(_service.SearchProducts(query));
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] CreateProductRequest request)
        {
            try
            {
                ProductModel model = new()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    SKU = request.SKU,
                    CategoryId = request.CategoryId
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
        public IActionResult Update(int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                _service.UpdateBasicInfo(
                    id,
                    request.Name,
                    request.Description,
                    request.Price,
                    request.CategoryId
                );

                return Ok("product updated successfully");
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("{id:int}/Activate")]
        public IActionResult Activate(int id)
        {
            try
            {
                _service.Activate(id);
                return Ok("product activated");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("{id:int}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            try
            {
                _service.Deactivate(id);
                return Ok("product deactivated");
            }
            catch (AppException ex)
            {              
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpDelete("{id:int}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("product deleted successfully");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
