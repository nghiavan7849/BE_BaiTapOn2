using BE_BTO2_Demo.DBContext;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE_BTO2_Demo.Repositories
{
    public class AllowAccessRepository : IAllowAccessRepository
    {
        private readonly MyDBContext _context;
        public AllowAccessRepository(MyDBContext context) {
            _context = context;
        }

        public async Task<IEnumerable<AllowAccess>> GetAllAllowAccess()
        {
            return await _context.AllowAccess.ToListAsync();
        }
        public async Task<AllowAccess?> GetAllowAccessById(int id)
        {
            return await _context.AllowAccess.FindAsync(id);
        }

        public async Task<AllowAccess> CreateAllowAccess(AllowAccess allowAccess)
        {
            await _context.AllowAccess.AddAsync(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }

        public async Task<AllowAccess> UpdateAllowAccess(AllowAccess allowAccess)
        {
            _context.AllowAccess.Update(allowAccess);
            await _context.SaveChangesAsync();
            return allowAccess;
        }


        public async Task<bool> DeleteAllowAccess(int id)
        {
            var allowAccess = await GetAllowAccessById(id);
            if (allowAccess == null) return false;
           
            _context.AllowAccess.Remove(allowAccess);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
