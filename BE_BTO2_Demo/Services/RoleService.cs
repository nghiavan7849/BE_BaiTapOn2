using AutoMapper;
using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Mappers;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using BE_BTO2_Demo.Services.Interfaces;

namespace BE_BTO2_Demo.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository roleRepository, IMapper mapper) 
        { 
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<RoleResponse>>> GetAllRole(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder)
        {
            var query = await _roleRepository.GetAllRole();
            if (!string.IsNullOrEmpty(search)) { 
                query = query.Where(r => r.RoleName.ToLower().Contains(search.ToLower()));
            }

            query = sortCollumn?.ToLower() switch
            {
                "roleid" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.RoleId) : query.OrderBy(u => u.RoleId),
                "rolename" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.RoleName) : query.OrderBy(u => u.RoleName),
                _ => query.OrderBy(u => u.RoleId)
            };
            int totalRecords = query.Count();
             
            var Roles = query
                .Skip((pageCurrent - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = _mapper.Map<List<RoleResponse>>(Roles);

            return ApiResponse<List<RoleResponse>>.Success(
                data: response,
                pageTotal: (int)Math.Ceiling(totalRecords/(double)pageSize),
                pageSize: pageSize,
                pageCurrent: pageCurrent,
                total:totalRecords
             );
        }

        public async Task<ApiResponse<RoleResponse>> GetRoleById(int id)
        {
            var Role = await _roleRepository.GetRoleById(id);
            if (Role == null) return ApiResponse<RoleResponse>.NotFound($"Role có id {id} không tồn tại!!!");

            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(Role));
        }

        public async Task<ApiResponse<RoleResponse>> CreateRole(RoleRequest request)
        {
            var Role = await _roleRepository.CreateRole(_mapper.Map<Role>(request));
            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(Role));
        }

        public async Task<ApiResponse<RoleResponse>> UpdateRole(int id, RoleRequest request)
        {

            var Role = await _roleRepository.GetRoleById(id);
            if (Role == null) return ApiResponse<RoleResponse>.NotFound($"Role có id {id} không tồn tại!!!");
            _mapper.Map(request, Role);
            var update = await _roleRepository.UpdateRole(Role);
            return ApiResponse<RoleResponse>.Success(_mapper.Map<RoleResponse>(update));
        }


        public async Task<ApiResponse<string>> DeleteRole(int id)
        {
            var deleted = await _roleRepository.DeleteRole(id);
            if (deleted)
            {
                return ApiResponse<string>.Success(default,"Xóa role thàng công");
            } else
            {
                return ApiResponse<string>.NotFound($"Role có id {id} không tồn tại!!!");
            }
        }
        
    }
}
