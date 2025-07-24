using GadgetHub.Dtos.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Orders
{
    public class PendingModel : PageModel
    {
        private readonly HttpClient _http;

        public PendingModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("GadgetHubAPI");
        }

        public List<CustomerOrderDto> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            Orders = await _http.GetFromJsonAsync<List<CustomerOrderDto>>("Order/PendingOrders");
        }
    }
}