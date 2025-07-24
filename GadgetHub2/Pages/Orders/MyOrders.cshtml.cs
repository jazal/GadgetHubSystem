using GadgetHub.Web.Models;
using GadgetHub.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub.Web.Pages.Orders
{
    public class MyOrdersModel : PageModel
    {
        private readonly OrderService _orderService;

        public MyOrdersModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        public List<OrderViewModels> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            int customerId = 6; // ✅ Hardcoded now, later replace with logged-in user
            Orders = await _orderService.GetCustomerOrdersAsync(customerId);
        }
    }
}
