using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_BTO2_Demo.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var jwt_key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY"));
            var jwt_issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var jwt_audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var jwt_expireMinutes = Environment.GetEnvironmentVariable("JWT_ExpireMinutes");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName ?? "NoFullname"),
                new Claim(ClaimTypes.Email, user.Email ?? "NoEmail"),
                new Claim(ClaimTypes.Role, user.Role.RoleName?? "NoRole")
            };

            var token = new JwtSecurityToken(
                issuer: jwt_issuer,
                audience: jwt_audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt_expireMinutes)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(jwt_key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
