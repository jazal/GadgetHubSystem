using GadgetHub2.API.Models;

namespace GadgetHub2.API.DTOs.Users;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public UserType UserType { get; set; }

}
