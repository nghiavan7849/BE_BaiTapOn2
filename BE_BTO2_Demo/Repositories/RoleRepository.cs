using BE_BTO2_Demo.DBContext;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE_BTO2_Demo.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly MyDBContext _context;
        public RoleRepository(MyDBContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Role?> GetRoleById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role> UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }


        public async Task<bool> DeleteRole(int id)
        {
            var role = await GetRoleById(id);
            if (role == null) return false;
           
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
