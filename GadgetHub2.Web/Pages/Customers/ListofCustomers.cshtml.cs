using GadgetHub2.Web.Models;
using GadgetHub2.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.Web.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly CustomerService _customerServices;

        public List<CustomerDto> Customers { get; set; }

        public IndexModel(CustomerService customerServices)
        {
            _customerServices = customerServices;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _customerServices.DeleteAsync(id);
            return RedirectToPage(); // Refreshes the current page
        }
        public async Task OnGetAsync()
        {
            Customers = await _customerServices.GetAllAsync();
        }
    }
}