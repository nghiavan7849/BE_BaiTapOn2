using BE_BTO2_Demo.Models;

namespace BE_BTO2_Demo.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        public Task<IEnumerable<Role>> GetAllRole();
        public Task<Role?> GetRoleById(int id);
        public Task<Role> CreateRole(Role Role);
        public Task<Role> UpdateRole(Role Role);
        public Task<bool> DeleteRole(int id);

    }
}
