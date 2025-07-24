using GadgetHub2.API.DTOs.Order;
using GadgetHub2.API.DTOs.Quotations;
using GadgetHub2.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Orders
{
    public class ViewModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public ViewModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GadgetHubAPI");
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        [BindProperty]
        public List<CreateQuotationDto> QuotationInputs { get; set; }
        
        public List<CustomerOrderDto> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync($"order/{OrderId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Orders/PendingOrders");

            var response2 = await _httpClient.GetAsync($"Quotation/GetByOrderId?orderId={OrderId}");

            ViewData["quotations"] = await response2.Content.ReadFromJsonAsync<List<QuotationDto>>();

            OrderDetails = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            // Model binding has updated QuotationInputs with form values
            foreach (var q in QuotationInputs)
            {
                // Two-way binding effect: changes from view are reflected here
            }

            return Page(); // or Redirect
        }

    }
}
