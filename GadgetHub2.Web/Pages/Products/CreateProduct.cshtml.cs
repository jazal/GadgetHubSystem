using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductService _productService;

        public CreateModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductDto Product { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            await _productService.CreateAsync(Product);
            return RedirectToPage("ListofProducts");
        }
    }
}