using GadgetHub2.WEB.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Cart;

public class IndexModel : PageModel
{
    public List<CartItem> CartItems { get; set; } /*= new();*/

    public void OnGet()
    {
        // Simulate loading cart items (in real apps use session or DB)
        CartItems = new List<CartItem>
        {
            new CartItem { ProductId = 1, ProductName = "Phone X", Quantity = 2 },
            new CartItem { ProductId = 3, ProductName = "Wireless Earbuds", Quantity = 1 }
        };
    }
}
