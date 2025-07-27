using GadgetHub.Dtos.Enums;
using GadgetHub.Dtos.OrderItems;
using GadgetHub.Dtos.Quotations;

namespace GadgetHub.Dtos.Order;

public class CustomerOrderDto
{
    public int? CustomerId { get; set; }

    public string CustomerName { get; set; }

    public string? CustomerEmail { get; set; }

    public int OrderId { get; set; }

    public decimal? TotalAmount { get; set; }

    #region Distributor

    public int? QuotationId { get; set; }

    public string? DistributorName { get; set; }

    public string? ApiUrl { get; set; }

    public DateTime? AssignedOn { get; set; }

    #endregion

    public DateTime? CreatedOn { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemDto> OrderItems { get; set; }

    /// <summary>
    /// REMOVE
    /// </summary>
    public List<QuotationDto> Quotations { get; set; }

    public CustomerOrderDto()
    {
        OrderItems = new List<OrderItemDto>();
        Quotations = new List<QuotationDto>();
    }
}
