using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
        public UserDto? LoggedInUser { get; private set; }

        public async Task OnGetAsync()
        {
            var payload = new FilterOrderDto();

            GetCurrentUser();

            if (LoggedInUser is null)
            {
                payload.CustomerId = 0;
            }
            else if (LoggedInUser.UserType == Dtos.Enums.UserType.Customer)
            {
                payload.CustomerId = LoggedInUser.Id;
            }

            var response = await _http.PostAsJsonAsync("Order/GetAll", payload);

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

        private void GetCurrentUser()
        {
            if (HttpContext.Request.Cookies.ContainsKey("currentUser"))
            {
                var userJson = HttpContext.Request.Cookies["currentUser"];
                if (!string.IsNullOrEmpty(userJson))
                {
                    LoggedInUser = System.Text.Json.JsonSerializer.Deserialize<UserDto>(userJson);
                }
            }
        }
    }
}
