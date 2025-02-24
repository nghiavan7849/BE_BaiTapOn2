using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Repositories.Interfaces;
using BE_BTO2_Demo.Services.Interfaces;

namespace BE_BTO2_Demo.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<ApiResponse<string>> Login(LoginRequest request)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return ApiResponse<string>.Error("Email hoặc mật khẩu không đúng!!!");

            var token = _jwtService.GenerateToken(user);
            return ApiResponse<string>.Success(token);
        }
    }
}
