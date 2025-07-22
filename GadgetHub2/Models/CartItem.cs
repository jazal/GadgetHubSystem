namespace GadgetHub2.WEB.Models;

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }  // Optional (for display)
    public int Quantity { get; set; }
}
