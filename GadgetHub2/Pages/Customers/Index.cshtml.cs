using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Customers;

public class IndexModel : PageModel
{
    private readonly UserService _UserService;

    public IndexModel(UserService UserService)
    {
        _UserService = UserService;
    }

    public List<CustomerViewModel> Customers { get; set; }

    public async Task OnGetAsync()
    {
        var users = await _UserService.GetAllAsync();
        Customers = users.Where(u => u.UserType == 2).ToList(); // only customers
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _UserService.DeleteAsync(id);
        return RedirectToPage();
    }
}
