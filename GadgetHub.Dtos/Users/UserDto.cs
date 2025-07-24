using GadgetHub.Dtos.Enums;

namespace GadgetHub.Dtos.Users;

public class UserDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Email { get; set; }

    public UserType UserType { get; set; }

}
