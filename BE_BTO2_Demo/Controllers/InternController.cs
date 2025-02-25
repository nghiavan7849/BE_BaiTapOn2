using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Timers;

namespace BE_BTO2_Demo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/intern")]
    public class InternController: ControllerBase
    {
        private readonly IInternService _internService;

        public InternController(IInternService internService)
        {
            _internService = internService;
        }
        [HttpGet]   
        
        public async Task<IActionResult> GetIntern(int pageCurrent = 1, int pageSize = 10)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();
            var response = await _internService.GetInternAsync(int.Parse(userId), pageSize, pageCurrent);
            if(response.Code == 0) return Ok(response);
            else if(response.Code == 2) return NotFound(response);
            else if (response.Code == 3) return Forbid();
            else return BadRequest(response);
        }

    }
}
