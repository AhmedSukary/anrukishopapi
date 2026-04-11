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
    public class InventoryController : Controller
    {
        private readonly IInventoryService _service;

        public InventoryController(IInventoryService service)
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

        [HttpGet("GetByProductId/{id:int}")]
        public IActionResult GetByProduct(int id)
        {
            try
            {
                return Ok(_service.GetByProductId(id));
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpPost("Create")]
        public IActionResult Create(CreateInventoryRequest request)
        {
            try
            {
                InventoryModel model = new()
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Location = request.Location
                };

                int newId = _service.Create(model);

                model = _service.GetById(newId);

                return Ok(model);
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }

        [HttpPut("{id:int}/Increase")]
        public IActionResult Increase(int id, StockChangeRequest request)
        {
            _service.IncreaseStock(id, request.Amount);
            return NoContent();
        }

        [HttpPut("{id:int}/Decrease")]
        public IActionResult Decrease(int id, StockChangeRequest request)
        {
            _service.DecreaseStock(id, request.Amount);
            return NoContent();
        }

        [HttpPut("{id:int}/Location")]
        public IActionResult ChangeLocation(int id, ChangeLocationRequest request)
        {
            _service.ChangeLocation(id, request.Location);
            return NoContent();
        }

        [HttpPut("{id:int}/Quantity")]
        public IActionResult SetQuantity(int id, SetQuantityRequest request)
        {
            _service.SetQuantity(id, request.Quantity);
            return NoContent();
        }

        [HttpDelete("{id:int}/Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("inventory deleted successfully");
            }
            catch (AppException ex)
            {
                return NotFound(new { ex.Code, ex.Message });
            }
        }
    }
}
