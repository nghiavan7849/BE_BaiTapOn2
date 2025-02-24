using BE_BTO2_Demo.Models;

namespace BE_BTO2_Demo.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUser();
        public Task<User?> GetById(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(User user);
        public Task<bool> DeleteUser(int id);

    }
}
