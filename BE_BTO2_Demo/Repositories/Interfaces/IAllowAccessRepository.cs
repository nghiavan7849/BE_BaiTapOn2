using BE_BTO2_Demo.Models;

namespace BE_BTO2_Demo.Repositories.Interfaces
{
    public interface IAllowAccessRepository
    {
        public Task<IEnumerable<AllowAccess>> GetAllAllowAccess();
        public Task<AllowAccess?> GetAllowAccessById(int id);
        public Task<AllowAccess> CreateAllowAccess(AllowAccess allowAccess);
        public Task<AllowAccess> UpdateAllowAccess(AllowAccess allowAccess);
        public Task<bool> DeleteAllowAccess(int id);

    }
}
