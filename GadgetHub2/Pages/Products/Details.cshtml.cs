using GadgetHub.Web.Models;
using GadgetHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductService _productService;

        public DetailsModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductViewModel Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return RedirectToPage("/Index");

            Product = product;
            return Page();
        }

        public IActionResult OnPost(int ProductId)
        {
            // ✅ Add to Cart using Session
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == ProductId);
            if (existingItem != null)
                existingItem.Quantity++;
            else
                cart.Add(new CartItem { ProductId = ProductId, ProductName = Product.Name, Price = Product.Price ?? 0, Quantity = 1 });

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToPage("/Cart/Index");
        }
    }
}
