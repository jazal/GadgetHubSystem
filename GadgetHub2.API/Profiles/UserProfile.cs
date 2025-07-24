using AutoMapper;
using GadgetHub.API.Models;
using GadgetHub.Dtos.Users;

namespace GadgetHub.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap();
        CreateMap<UpdateUserDto, User>().ReverseMap();
    }
}