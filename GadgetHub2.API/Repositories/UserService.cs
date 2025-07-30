using GadgetHub.API.Data;
using GadgetHub.Dtos.Users;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Repositories;

public class UserService
{
    private readonly GadgetHubContext _context;

    public UserService(GadgetHubContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Login(LoginDto input)
    {
        var user = await _context.Users
            .Where(x => x.Email.Equals(input.Email) && x.Password == input.Password)
            .FirstOrDefaultAsync();

        if (user is null) return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            UserType = user.UserType
        };
    }
}
