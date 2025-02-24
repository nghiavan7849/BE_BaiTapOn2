using AutoMapper;
using BE_BTO2_Demo.DTOs.Request;
using BE_BTO2_Demo.DTOs.Response;
using BE_BTO2_Demo.Models;

namespace BE_BTO2_Demo.Mappers
{
    public class UserMapper: Profile
    {
        public UserMapper() 
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserRequest, User>();
        }
    }
}
