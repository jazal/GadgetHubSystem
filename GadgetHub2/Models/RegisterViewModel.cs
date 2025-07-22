namespace GadgetHub2.WEB.Models;

public class RegisterViewModel
{
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public byte UserType { get; set; }  // 1 = Distributor, 2 = Customer
}

