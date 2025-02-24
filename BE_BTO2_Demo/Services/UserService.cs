using AutoMapper;
using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Mappers;
using BE_BTO2_Demo.Models;
using BE_BTO2_Demo.Repositories.Interfaces;
using BE_BTO2_Demo.Services.Interfaces;

namespace BE_BTO2_Demo.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper) 
        { 
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<UserResponse>>> GetAllUser(int pageSize, int pageCurrent, string? search, string? sortCollumn, string? sortOrder)
        {
            var query = await _userRepository.GetAllUser();
            if (!string.IsNullOrEmpty(search)) { 
                query = query.Where(u => u.FullName.ToLower().Contains(search.ToLower()));
            }

            query = sortCollumn?.ToLower() switch
            {
                "userid" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.UserId) : query.OrderBy(u => u.UserId),
                "fullname" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.FullName) : query.OrderBy(u => u.FullName),
                "email" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.Email) : query.OrderBy(u => u.Email),
                "birthdate" => sortOrder?.ToLower() == "desc" ? query.OrderByDescending(u => u.BirthDate) : query.OrderBy(u => u.BirthDate),
                _ => query.OrderBy(u => u.UserId)
            };
            int totalRecords = query.Count();
             
            var users = query
                .Skip((pageCurrent - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = _mapper.Map<List<UserResponse>>(users);

            return ApiResponse<List<UserResponse>>.Success(
                data: response,
                pageTotal: (int)Math.Ceiling(totalRecords/(double)pageSize),
                pageSize: pageSize,
                pageCurrent: pageCurrent,
                total:totalRecords
             );
        }

        public async Task<ApiResponse<UserResponse>> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null) return ApiResponse<UserResponse>.NotFound($"User có id {id} không tồn tại!!!");

            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(user));
        }

        public async Task<ApiResponse<UserResponse>> CreateUser(UserRequest request)
        {
            var role = await _roleRepository.GetRoleById(request.RoleId);
            if (role == null) return ApiResponse<UserResponse>.NotFound($"Role có id {request.RoleId} không tồn tại!!!");

            var user = await _userRepository.CreateUser(_mapper.Map<User>(request));
            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(user));
        }

        public async Task<ApiResponse<UserResponse>> UpdateUser(int id, UserRequest request)
        {
            var role = await _roleRepository.GetRoleById(request.RoleId);
            if (role == null) return ApiResponse<UserResponse>.NotFound($"Role có id {request.RoleId} không tồn tại!!!");

            var user = await _userRepository.GetById(id);
            if (user == null) return ApiResponse<UserResponse>.NotFound($"User có id {id} không tồn tại!!!");
            _mapper.Map(request, user);
            var update = await _userRepository.UpdateUser(user);
            return ApiResponse<UserResponse>.Success(_mapper.Map<UserResponse>(update));
        }


        public async Task<ApiResponse<string>> DeleteUser(int id)
        {
            var deleted = await _userRepository.DeleteUser(id);
            if (deleted)
            {
                return ApiResponse<string>.Success(default,"Xóa user thàng công");
            } else
            {
                return ApiResponse<string>.NotFound($"User có id {id} không tồn tại!!!");
            }
        }
        
    }
}
