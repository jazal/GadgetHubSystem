using GadgetHub.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace GadgetHub.Web.Pages.Cart
{
    public class ConfirmationModel : PageModel
    {
        public List<QuotationResponse> OrderSummary { get; set; }

        public void OnGet()
        {
            if (TempData["OrderSummary"] is string json)
            {
                OrderSummary = JsonSerializer.Deserialize<List<QuotationResponse>>(json);
            }
            else
            {
                OrderSummary = new List<QuotationResponse>();
            }
        }
    }
}
