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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
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
        [HttpGet("GetCategoryTree")]
        public IActionResult GetCategoryTree()
        {
            return Ok(_service.GetCategoryTree());
        }

        [AllowAnonymous]
        [HttpGet("GetCategoryPathBy/{id:int}")]
        public IActionResult GetCategoryPathById(int id)
        {
            try
            {
                return Ok(new { Path = _service.GetCategoryPathById(id) });
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public IActionResult Create(CreateCategoryRequest request)
        {
            try
            {
                CategoryModel model = new()
                {
                    Name = request.Name,
                    ParentCategoryId = request.ParentCategoryId
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

        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id:int}")]
        public IActionResult Update(int id, UpdateCategoryRequest request)
        {
            try
            {
                _service.UpdateBasicInfo(
                    id,
                    request.Name,
                    request.ParentCategoryId
                );

                return Ok("category updated successfully");
            }
            catch (AppException ex)
            {
                return BadRequest(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/Activate")]
        public IActionResult Activate(int id)
        {
            try
            {
                _service.Activate(id);
                return Ok("category activated");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            try
            {
                _service.Deactivate(id);
                return Ok("category deactivated");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("category deleted successfully");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
