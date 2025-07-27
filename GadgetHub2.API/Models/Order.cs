using GadgetHub.Dtos.Enums;

namespace GadgetHub.API.Models;

public class Order
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedOn { get; set; }

    #region Distributor

    public int? QuotationId { get; set; }

    public string? DistributorName { get; set; }

    public string? ApiUrl { get; set; }

    public DateTime? AssignedOn { get; set; }

    #endregion

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItem> OrderItems { get; set; }

    public Order()
    {
        OrderItems = new List<OrderItem>();
    }
}

