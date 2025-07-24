using GadgetHub2.WEB.Services;
using GadgetHub2.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Products;

public class EditModel : PageModel
{
    private readonly ProductService _productService;

    public EditModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Product = await _productService.GetByIdAsync(id);
        if (Product == null)
            return RedirectToPage("Index");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _productService.UpdateAsync(Product.Id, Product);
        return RedirectToPage("Index");
    }
}

