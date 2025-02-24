using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BE_BTO2_Demo.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageSize = 10, int pageCurrent = 1, string? search = "", string? sortCollumn = "UserId", string? sortOrder = "asc")
        {
            var response = await _userService.GetAllUser(pageSize, pageCurrent, search, sortCollumn, sortOrder);
            if(response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserById(id);
            if (response.Code == 0) return Ok(response);
            else if (response.Code == 2) return NotFound(response);
            else return BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserRequest request)
        {
            var response = await _userService.CreateUser(request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id,UserRequest request)
        {
            var response = await _userService.UpdateUser(id, request);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Code == 0) return Ok(response);
            else return BadRequest(response);

        }
    }
}
