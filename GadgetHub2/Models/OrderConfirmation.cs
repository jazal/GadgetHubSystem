namespace GadgetHub.Web.Models;

public class OrderConfirmation
{

    public string DistributorName { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }  = DateTime.UtcNow;
}
