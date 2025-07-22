namespace GadgetHub2.WEB.Models;

public class OrderViewModels
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime? CreatedOn { get; set; }
    public byte OrderStatus { get; set; }
}
