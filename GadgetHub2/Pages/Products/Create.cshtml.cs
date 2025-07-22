using GadgetHub2.WEB.Services;
using GadgetHub2.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Products;

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

