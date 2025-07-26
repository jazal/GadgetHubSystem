using GadgetHub.Dtos.Enums;

namespace GadgetHub.Dtos.Distributors;

public class QuotationsViewModel
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public int GadgetHubOrderId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? EstimatedDeliveryDateTime { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime CreatedOn { get; set; }

    public List<QuotationItemDto> QuotationItems { get; set; }

    public QuotationsViewModel()
    {
        QuotationItems = new List<QuotationItemDto>();
    }
}

public class QuotationItemDto
{
    public int Id { get; set; }

    public int QuotationId { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
