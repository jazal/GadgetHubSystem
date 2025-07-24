namespace GadgetHub2.API.DTOs.Quotations;

public class QuotationDto
{
    public int Id { get; set; }

    public int DistributorId { get; set; }

    public int OrderId { get; set; }

    public int OrderItemId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int EstimatedDeliveryDays { get; set; }

    public DateTime? CreatedOn { get; set; }
}
