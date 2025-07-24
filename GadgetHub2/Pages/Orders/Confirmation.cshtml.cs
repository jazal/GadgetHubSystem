using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub.Web.Pages.Orders
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet(string message)
        {
            Message = message ?? "Order confirmed!";
        }
    }
}

