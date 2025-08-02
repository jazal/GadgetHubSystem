using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.Users;
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
        public UserDto? LoggedInUser { get; private set; }

        public async Task OnGetAsync()
        {
            var payload = new FilterOrderDto();

            //  Get logged-in user from cookie
            GetCurrentUser();

            //  Check user type and apply filter accordingly
            if (LoggedInUser is null)
            {
                // If no user is logged in → return empty or CustomerId = 0
                payload.CustomerId = 0;
            }
            else if (LoggedInUser.UserType == Dtos.Enums.UserType.Customer)
            {
                //  Customer → only see their own orders
                payload.CustomerId = LoggedInUser.Id;
            }
            else if (LoggedInUser.UserType == Dtos.Enums.UserType.Admin)
            {
                // Admin → see ALL orders (do NOT filter by CustomerId)
                // Leave payload.CustomerId as null (assuming API handles null = all orders)
                payload.CustomerId = null;
            }

            // Send request to API
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
                //  If API call fails → set empty list (or show error)
                Orders = new List<CustomerOrderDto>();
            }
        }

        private void GetCurrentUser()
        {
            //  Read user details from cookie
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
