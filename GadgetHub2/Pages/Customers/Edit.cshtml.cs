using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GadgetHub.Web.Models;
using GadgetHub.Web.Services;

namespace GadgetHub.Web.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly UserService _UserService;

        public EditModel(UserService UserService)
        {
            _UserService = UserService;
        }

        [BindProperty]
        public CustomerViewModel Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Customer = await _UserService.GetByIdAsync(id);
            if (Customer == null || Customer.UserType != 2)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Customer.UserType = 2; //  Customer type
            await _UserService.UpdateAsync(Customer.Id, Customer);

            return RedirectToPage("Index");
        }
    }
}
