using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Products;

public class CreateModel : PageModel
{
    private readonly ProductService _productService;

    public CreateModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; }

    public void OnGet()
    {
        // optional: init values if needed
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Product.CreatedOn = DateTime.Now;
        await _productService.CreateAsync(Product);

        return RedirectToPage("Index");
    }
}

