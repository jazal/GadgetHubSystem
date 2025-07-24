using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Customers;

public class CreateModel : PageModel
{
    private readonly UserService _UserService;

    public CreateModel(UserService UserService)
    {
        _UserService = UserService;
    }

    [BindProperty]
    public CustomerViewModel Customer { get; set; }

    public void OnGet()
    {
        // nothing needed
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Customer.UserType = 2; // Ensure it's Customer

        await _UserService.CreateAsync(Customer);
        return RedirectToPage("Index");
    }
}
