using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly CustomerService _customerService;

        [BindProperty]
        public CustomerDto Customer { get; set; }

        public CreateModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public void OnGet()
        {
            // Just render empty form
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _customerService.CreateAsync(Customer);

            if (response.IsSuccessStatusCode)
                return RedirectToPage("ListofProducts");

            // Optionally: show error
            ModelState.AddModelError(string.Empty, "Error creating customer.");
            return Page();
        }
    }
}
