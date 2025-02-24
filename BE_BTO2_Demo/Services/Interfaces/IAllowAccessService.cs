using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IAllowAccessService
    {
        public Task<ApiResponse<List<AllowAccessResponse>>> GetAllAllowAccess(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder);
        public Task<ApiResponse<AllowAccessResponse>> GetAllowAccessById(int id);
        public Task<ApiResponse<AllowAccessResponse>> CreateAllowAccess(AllowAccessRequest request);
        public Task<ApiResponse<AllowAccessResponse>> UpdateAllowAccess(int id, AllowAccessRequest request);
        public Task<ApiResponse<string>> DeleteAllowAccess(int id);
    }
}
