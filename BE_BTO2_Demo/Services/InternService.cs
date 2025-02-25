using BE_BTO2_Demo.DBContext;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using BE_BTO2_Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BE_BTO2_Demo.Services
{
    public class InternService: IInternService
    {
        private readonly IUserRepository _userRepository;
        private readonly MyDBContext _context;

        public InternService(IUserRepository userRepository, MyDBContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<ApiResponse<object>> GetInternAsync(int userId, int pageSize, int pageCurrent)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null) return ApiResponse<object>.NotFound($"User có id {userId} không tồn tại!!!");

            var roleId = user.RoleId;

            var allowAccess = await _context.AllowAccess
                                        .Where(a => a.RoleId == roleId && a.TableName == "Intern")
                                        .Select(a => a.AccessProperties)
                                        .FirstOrDefaultAsync();
            if (allowAccess == null) return ApiResponse<object>.Forbidden();
            var allowAccessColumns = allowAccess.Split(",").Select(a => a.Trim()).ToList(); 

            var totalRecords = await _context.Interns.CountAsync();

            var interns = await _context.Interns
                                    .Skip((pageCurrent -1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            var results = interns.Select(intern =>
            {
                var obj = new Dictionary<string, object>();

                var properties = typeof(Intern).GetProperties();

                foreach (var prop in properties) 
                {
                    if (allowAccessColumns.Contains(prop.Name))
                    {
                        obj[prop.Name] = prop.GetValue(intern);
                    }
                }

                return obj;
            }).ToList();

            return ApiResponse<object>.Success(
                    data: results,
                    pageTotal: (int)Math.Ceiling((double)totalRecords/pageSize),
                    pageSize: pageSize,
                    pageCurrent: pageCurrent,
                    total: totalRecords
                );
        }
    }
}
