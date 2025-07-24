using AutoMapper;
using GadgetHub.Dtos.Users;
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