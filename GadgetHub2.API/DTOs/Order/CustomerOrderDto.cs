using GadgetHub2.API.DTOs.OrderItems;
using GadgetHub2.API.DTOs.Quotations;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.DTOs.Order;

public class CustomerOrderDto
{
    public int? CustomerId { get; set; }

    public string CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public int OrderId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemDto> OrderItems { get; set; }

    public List<QuotationDto> Quotations { get; set; }

    public CustomerOrderDto()
    {
        OrderItems = new List<OrderItemDto>();
        Quotations = new List<QuotationDto>();
    }
}
