using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly OrderService _orderService;

        public DetailsModel(OrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public OrderDetailsViewModel OrderDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            OrderDetails = await _orderService.GetOrderByIdAsync(OrderId);
            if (OrderDetails == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
