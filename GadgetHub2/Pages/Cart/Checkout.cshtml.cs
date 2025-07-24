using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly CartService _cartService;

        public CheckoutModel(CartService cartService)
        {
            _cartService = cartService;
        }

        [BindProperty]
        public List<CartItem> CartItems { get; set; }

        public List<QuotationResponse> Quotations { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            // Dummy cart again for now; later we’ll use session/persistence
            CartItems = new List<CartItem>
            {
                new CartItem { ProductId = 1, Quantity = 2 },
                new CartItem { ProductId = 3, Quantity = 1 }
            };

            Quotations = await _cartService.GetQuotationsAsync(CartItems);
            return Page();
        }

        public async Task<IActionResult> OnPostPlaceOrderAsync()
        {
            var selectedQuotes = new List<QuotationResponse>
            {
                // For simplicity, send all quotes — later let user choose one per product
               new QuotationResponse
        {
            ProductId = 1,
            DistributorName = "TechWorld",
            Price = 120,
            AvailableQuantity = 5,
            EstimatedDeliveryDays = 2
        },
             new QuotationResponse
        {
            ProductId = 3,
            DistributorName = "Gadget Central",
            Price = 60,
            AvailableQuantity = 10,
            EstimatedDeliveryDays = 3
        }
    };

            await _cartService.PlaceOrderAsync(selectedQuotes);

            TempData["OrderSummary"] = System.Text.Json.JsonSerializer.Serialize(selectedQuotes);

            return RedirectToPage("Confirmation");
        }
        }
    }
    

