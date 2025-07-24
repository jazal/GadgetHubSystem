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

    public List<ProductViewModel> Products { get; set; }

    public async Task OnGetAsync()
    {
        Products = await _productService.GetAllAsync();
    }

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        await _productService.DeleteAsync(id);
        return RedirectToPage();
    }
}
