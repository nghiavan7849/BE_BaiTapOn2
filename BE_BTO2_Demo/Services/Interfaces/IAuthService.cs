using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ApiResponse<string>> Login(LoginRequest request);
    }
}
