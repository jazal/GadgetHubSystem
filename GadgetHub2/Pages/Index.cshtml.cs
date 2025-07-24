//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace GadgetHub2.Pages
//{
//    public class IndexModel : PageModel
//    {
//        private readonly ILogger<IndexModel> _logger;

//        public IndexModel(ILogger<IndexModel> logger)
//        {
//            _logger = logger;
//        }

//        public void OnGet()
//        {

//        }
//    }
//}

using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService _productService;

        public IndexModel(ProductService productService)
        {
            _productService = productService;
        }

        public List<ProductViewModel> Products { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        public async Task OnGetAsync()
        {
            var allProducts = await _productService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
                allProducts = allProducts.Where(p => p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

            if (MinPrice.HasValue)
                allProducts = allProducts.Where(p => p.Price >= MinPrice.Value).ToList();

            if (MaxPrice.HasValue)
                allProducts = allProducts.Where(p => p.Price <= MaxPrice.Value).ToList();

            Products = allProducts;
        }

        // Handles Add to Cart (to be implemented next)
        public async Task<IActionResult> OnPostAddToCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            // ✅ Fetch product details from API
            var product = await _productService.GetByIdAsync(id);
            if (product != null)
            {
                var existingItem = cart.FirstOrDefault(c => c.ProductId == id);
                if (existingItem != null)
                {
                    existingItem.Quantity += 1;
                }
                else
                {
                    cart.Add(new CartItem
        {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price ?? 0,  // ✅ Price added
                        Quantity = 1
                    });
                }

                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToPage("/Cart/Index");
        }
    }
}



