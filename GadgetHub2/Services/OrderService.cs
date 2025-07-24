using GadgetHub.Web.Models;

namespace GadgetHub.Web.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        //  Place Order
        public async Task<bool> CreateOrderAsync(OrderDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Order/PlaceOrder", order);
            return response.IsSuccessStatusCode;
        }

        //  Get Orders and filter by Customer
        public async Task<List<OrderViewModels>> GetCustomerOrdersAsync(int customerId)
        {
            // Fetch all orders from API
            var allOrders = await _httpClient.GetFromJsonAsync<List<OrderViewModels>>("api/order");

            // Filter orders for the logged-in customer
            return allOrders?.Where(o => o.CustomerId == customerId).ToList() ?? new List<OrderViewModels>();
        }

        internal async Task<OrderDetailsViewModel> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
