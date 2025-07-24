using System.ComponentModel.DataAnnotations;

namespace GadgetHub2.WEB.Models;

public class OrderViewModels
{
    [Key]
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime? CreatedOn { get; set; }
    public byte OrderStatus { get; set; }

}
