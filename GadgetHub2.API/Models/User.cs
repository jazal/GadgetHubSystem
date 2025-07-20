namespace GadgetHub2.API.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public UserType UserType { get; set; }
}

public enum UserType : byte
{
    Distributor = 1,
    Customer = 2
}
