using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly CustomerService _customerService;

        public EditModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public CustomerDto Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _customerService.GetByIdAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _customerService.UpdateAsync(Customer);
            return RedirectToPage("ListofCustomers");
        }
    }
}