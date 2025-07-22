using GadgetHub2.WEB.Services;
using GadgetHub2.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Customers;

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
