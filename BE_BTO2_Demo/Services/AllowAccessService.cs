using AutoMapper;
using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Mappers;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using BE_BTO2_Demo.Services.Interfaces;

namespace BE_BTO2_Demo.Services
{
    public class AllowAccessService : IAllowAccessService
    {
        private readonly IAllowAccessRepository _allowAccessRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public AllowAccessService(IAllowAccessRepository allowAccessRepository, IRoleRepository roleRepository, IMapper mapper) 
        {
            _allowAccessRepository = allowAccessRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AllowAccessResponse>>> GetAllAllowAccess(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder)
        {
            var query = await _allowAccessRepository.GetAllAllowAccess();
            if (!string.IsNullOrEmpty(search)) { 
                query = query.Where(r => r.TableName.ToLower().Contains(search.ToLower()));
            }

            query = sortCollumn?.ToLower() switch
            {
                "id" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.Id) : query.OrderBy(u => u.Id),
                "tablename" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.TableName) : query.OrderBy(u => u.TableName),
                "accessproperties" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.AccessProperties) : query.OrderBy(u => u.AccessProperties),
                _ => query.OrderBy(u => u.Id)
            };
            int totalRecords = query.Count();
             
            var AllowAccesss = query
                .Skip((pageCurrent - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = _mapper.Map<List<AllowAccessResponse>>(AllowAccesss);

            return ApiResponse<List<AllowAccessResponse>>.Success(
                data: response,
                pageTotal: (int)Math.Ceiling(totalRecords/(double)pageSize),
                pageSize: pageSize,
                pageCurrent: pageCurrent,
                total:totalRecords
             );
        }

        public async Task<ApiResponse<AllowAccessResponse>> GetAllowAccessById(int id)
        {
            var AllowAccess = await _allowAccessRepository.GetAllowAccessById(id);
            if (AllowAccess == null) return ApiResponse<AllowAccessResponse>.NotFound($"AllowAccess có id {id} không tồn tại!!!");

            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(AllowAccess));
        }

        public async Task<ApiResponse<AllowAccessResponse>> CreateAllowAccess(AllowAccessRequest request)
        {
            var role = await _roleRepository.GetRoleById(request.RoleId);
            if (role == null) return ApiResponse<AllowAccessResponse>.NotFound($"Role có id {request.RoleId} không tồn tại!!!");

            var AllowAccess = await _allowAccessRepository.CreateAllowAccess(_mapper.Map<AllowAccess>(request));
            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(AllowAccess));
        }

        public async Task<ApiResponse<AllowAccessResponse>> UpdateAllowAccess(int id, AllowAccessRequest request)
        {
            var AllowAccess = await _allowAccessRepository.GetAllowAccessById(id);
            if (AllowAccess == null) return ApiResponse<AllowAccessResponse>.NotFound($"AllowAccess có id {id} không tồn tại!!!");
            
            var role = await _roleRepository.GetRoleById(request.RoleId);
            if (role == null) return ApiResponse<AllowAccessResponse>.NotFound($"Role có id {request.RoleId} không tồn tại!!!");

            _mapper.Map(request, AllowAccess);
            var update = await _allowAccessRepository.UpdateAllowAccess(AllowAccess);
            return ApiResponse<AllowAccessResponse>.Success(_mapper.Map<AllowAccessResponse>(update));
        }


        public async Task<ApiResponse<string>> DeleteAllowAccess(int id)
        {
            var deleted = await _allowAccessRepository.DeleteAllowAccess(id);
            if (deleted)
            {
                return ApiResponse<string>.Success(default,"Xóa AllowAccess thàng công");
            } else
            {
                return ApiResponse<string>.NotFound($"AllowAccess có id {id} không tồn tại!!!");
            }
        }
        
    }
}
