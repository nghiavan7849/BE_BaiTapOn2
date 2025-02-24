using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ApiResponse<List<UserResponse>>> GetAllUser(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder);
        public Task<ApiResponse<UserResponse>> GetUserById(int id);
        public Task<ApiResponse<UserResponse>> CreateUser(UserRequest request);
        public Task<ApiResponse<UserResponse>> UpdateUser(int id, UserRequest request);
        public Task<ApiResponse<string>> DeleteUser(int id);
    }
}
