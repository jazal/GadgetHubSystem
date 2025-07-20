using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductService _productService;

        public EditModel(ProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public ProductDto Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productService.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)

                return Page();


            await _productService.UpdateAsync(Product);
            return RedirectToPage("ListofProducts");
        }
    }
}