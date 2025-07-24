namespace GadgetHub.Web.Models;

public class QuotationResponse
{


    public int ProductId { get; set; }
    public string DistributorName { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
    public int EstimatedDeliveryDays { get; set; }

}
