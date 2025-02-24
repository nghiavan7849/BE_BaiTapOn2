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

        public async Task<Role> CreateRole(Role Role)
        {
            await _context.Roles.AddAsync(Role);
            await _context.SaveChangesAsync();
            return Role;
        }

        public async Task<Role> UpdateRole(Role Role)
        {
            _context.Roles.Update(Role);
            await _context.SaveChangesAsync();
            return Role;
        }


        public async Task<bool> DeleteRole(int id)
        {
            var Role = await GetRoleById(id);
            if (Role == null) return false;
           
            _context.Roles.Remove(Role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
