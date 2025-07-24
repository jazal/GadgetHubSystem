using GadgetHub.Dtos.Order;
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

        public List<CustomerOrderDto> OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _httpClient.GetAsync($"order/{OrderId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Orders/PendingOrders");

            OrderDetails = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();
            return Page();
        }
    }
}
