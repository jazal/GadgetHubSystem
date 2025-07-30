using GadgetHub.Web.Models;
using GadgetHub.Web.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
            var customerId = int.Parse(User.FindFirst("CustomerId").Value);
            Orders = await _orderService.GetCustomerOrdersAsync(customerId);
        }
    }

}       

