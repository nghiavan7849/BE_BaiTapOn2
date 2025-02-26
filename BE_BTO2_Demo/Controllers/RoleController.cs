using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTO2_Demo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/role")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize = 10, int pageCurrent = 1, 
            string? search = "", string? sortCollumn = "RoleId", string? sortOrder = "asc")
        {
            var response = await _roleService.GetAllRole(pageSize, pageCurrent, search, sortCollumn, sortOrder);
            if(response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _roleService.GetRoleById(id);
            if (response.Code == 0) return Ok(response);
            else if (response.Code == 2) return NotFound(response);
            else return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleRequest request)
        {
            var response = await _roleService.CreateRole(request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id,RoleRequest request)
        {
            var response = await _roleService.UpdateRole(id, request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await _roleService.DeleteRole(id);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);

        }
    }
}
