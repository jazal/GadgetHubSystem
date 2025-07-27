using GadgetHub.Dtos;
using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.OrderItems;
using GadgetHub.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub.Web.Pages.Orders
{
    public class QuotationsModel : PageModel
    {
        private readonly QuotationService _quotationService;
        private readonly OrderService _orderService;

        public QuotationsModel(QuotationService quotationService, OrderService orderService)
        {
            _quotationService = quotationService;
            _orderService = orderService;
        }

        public List<QuotationResponseDto> Quotations { get; set; } = new();

        public async Task OnGetAsync(int productId, int quantity)
        {
            var request = new QuotationRequestDto
            {
                ProductId = productId,
                Quantity = quantity
            };

            Quotations = await _quotationService.GetQuotationsAsync(request);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Pick cheapest distributor
            var bestQuote = Quotations.OrderBy(q => q.Price).FirstOrDefault();

            if (bestQuote != null)
            {
                var order = new OrderDto
                {
                    CustomerId = 6,
                    TotalAmount = bestQuote.Price,
                    OrderItems = new List<OrderItemDto>
                    {
                        new OrderItemDto { ProductId = 1, Quantity = 1, Price = bestQuote.Price }
                    }
                };

                //await _orderService.CreateOrderAsync(order);
            }

            return RedirectToPage("/Orders/MyOrders");
        }
    }
}
