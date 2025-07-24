using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly OrderService _orderService;

        public IndexModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public List<CartItem> CartItems { get; set; } = new();

        [TempData]
        public string Message { get; set; }  // ✅ For success/failure alerts

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        }

        public IActionResult OnPostUpdateQuantity(int id, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            cart.RemoveAll(c => c.ProductId == id);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (!cart.Any())
            {
                Message = "Your cart is empty!";
                return RedirectToPage();
            }

            // ✅ Create order payload
            var order = new OrderDto
            {
                CustomerId = 6, // ✅ Later use logged-in user ID
                OrderItems = cart.Select(c => new OrderItemDto
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName, // ✅ Include name
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList()
            };

            // ✅ Call API using injected _orderService
            var result = await _orderService.CreateOrderAsync(order);
            if (result)
            {
                HttpContext.Session.Remove("Cart");
                Message = "✅ Order created successfully!";
                return RedirectToPage("/Orders/Confirmation", new { message = "Order placed successfully!" });
            }

            Message = "❌ Failed to create order. Please try again.";
            return RedirectToPage();
        }
    }
}
