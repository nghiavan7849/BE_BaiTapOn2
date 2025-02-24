using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<ApiResponse<List<RoleResponse>>> GetAllRole(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder);
        public Task<ApiResponse<RoleResponse>> GetRoleById(int id);
        public Task<ApiResponse<RoleResponse>> CreateRole(RoleRequest request);
        public Task<ApiResponse<RoleResponse>> UpdateRole(int id, RoleRequest request);
        public Task<ApiResponse<string>> DeleteRole(int id);
    }
}
