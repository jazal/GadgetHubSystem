using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.Quotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub.Web.Pages.Orders
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
        public int SelectedQuotationId { get; set; }

        [BindProperty]
        public string SelectedApiUrlId { get; set; }

        [BindProperty]
        public string SelectedDistributorName { get; set; }

        [BindProperty]
        public List<ConfirmedQuotationItemDto> QuotationItems { get; set; } = new();

        public List<CustomerOrderDto> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync($"order/{OrderId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Orders/Index");

            OrderDetails = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();

            await GetDistributorQuoutations();

            return Page();
        }

        public async Task<IActionResult> OnPostConfirmQuotation()
        {
            // Example: Call API to place order using quotation details
            var request = new AssignQuotationDto
            {
                OrderId = OrderId,
                QuotationId = SelectedQuotationId,
                ApiUrlId = SelectedApiUrlId,
                DistributorName = SelectedDistributorName,
                Items = QuotationItems
            };

            // Call your Quotations API
            var response = await _httpClient.PostAsJsonAsync("Quotations/AssignQuotations", request);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Order confirmed successfully!";
                return RedirectToPage("/Orders/MyOrders");
            }

            TempData["Error"] = "Failed to confirm order.";
            return Page();
        }

        private async Task GetDistributorQuoutations()
        {
            var response = await _httpClient.GetAsync($"Quotations/GetAllByOrderId/{OrderId}");

            if (response.IsSuccessStatusCode)
            {
                ViewData["quotations"] = await response.Content.ReadFromJsonAsync<List<QuotationsViewModel>>();
            }
        }

    }
}
