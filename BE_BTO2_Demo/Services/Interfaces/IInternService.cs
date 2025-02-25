using BE_BTO2_Demo.DTOs.Response;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IInternService
    {
        public Task<ApiResponse<object>> GetInternAsync(int userId, int pageSize, int pageCurrent);
    }
}
