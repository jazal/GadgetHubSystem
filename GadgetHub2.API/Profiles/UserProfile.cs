using AutoMapper;
using GadgetHub2.API.DTOs.Users;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
    }
}