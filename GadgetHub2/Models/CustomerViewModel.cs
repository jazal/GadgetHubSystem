namespace GadgetHub2.WEB.Models;

public class CustomerViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }  // Only used for Register

    public byte UserType { get; set; } = 2;  // 2 = Customer
}

