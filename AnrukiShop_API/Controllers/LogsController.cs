using Microsoft.AspNetCore.Mvc;
using AnrukiShop_Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace AnrukiShop_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private readonly ILoggingService _service;

        public LogsController(ILoggingService service)
        {
            _service = service;
        }

        [HttpGet("GetLogs")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetLogs());
        }
    }
}
