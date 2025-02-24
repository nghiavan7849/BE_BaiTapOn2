using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTO2_Demo.Controllers
{
    [ApiController]
    [Route("api/allow-access")]
    public class AllowAccessController : ControllerBase
    {
        private readonly IAllowAccessService _allowAccessService;

        public AllowAccessController(IAllowAccessService allowAccessService)
        {
            _allowAccessService = allowAccessService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize = 10, int pageCurrent = 1, string? search = "", string? sortCollumn = "Id", string? sortOrder = "asc")
        {
            var response = await _allowAccessService.GetAllAllowAccess(pageSize, pageCurrent, search, sortCollumn, sortOrder);
            if(response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllowAccessById(int id)
        {
            var response = await _allowAccessService.GetAllowAccessById(id);
            if (response.Code == 0) return Ok(response);
            else if (response.Code == 2) return NotFound(response);
            else return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAllowAccess(AllowAccessRequest request)
        {
            var response = await _allowAccessService.CreateAllowAccess(request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAllowAccess(int id,AllowAccessRequest request)
        {
            var response = await _allowAccessService.UpdateAllowAccess(id, request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllowAccess(int id)
        {
            var response = await _allowAccessService.DeleteAllowAccess(id);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);

        }
    }
}
