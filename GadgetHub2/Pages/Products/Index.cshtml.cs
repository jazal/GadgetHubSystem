using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly ProductService _productService;

    public IndexModel(ProductService productService)
    {
        _productService = productService;
    }

    // ✅ Initialize the list to avoid null reference issues
    public List<ProductViewModel> Products { get; set; } = new();

    public async Task OnGetAsync()
    {
        // ✅ Ensure Products never becomes null
        Products = await _productService.GetAllAsync() ?? new List<ProductViewModel>();
    }

    // ✅ Change handler from GET to POST (because your form uses method="post")
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToPage();
    }
}
