using GadgetHub.Dtos.Order;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub.Web.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _http;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("GadgetHubAPI");
        }

        public List<CustomerOrderDto> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _http.PostAsJsonAsync("Order/GetAll", new FilterOrderDto());

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<CustomerOrderDto>>();
                if (data != null)
                {
                    Orders = data;
                }
            }
            else
            {
                // Handle error (optional)
                Orders = new List<CustomerOrderDto>();
            }
        }
    }
}