using BE_BTO2_Demo.Models;

namespace BE_BTO2_Demo.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
