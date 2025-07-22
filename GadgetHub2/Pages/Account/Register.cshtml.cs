using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly RegisterService _registerService;

    public RegisterModel(RegisterService registerService)
    {
        _registerService = registerService;
    }

    [BindProperty]
    public RegisterViewModel Register { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _registerService.RegisterAsync(Register);
        return RedirectToPage("/Customers/Index"); // Or redirect to Login page
    }
}
