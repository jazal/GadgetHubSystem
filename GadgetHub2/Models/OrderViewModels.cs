using System.ComponentModel.DataAnnotations;

namespace GadgetHub.Web.Models;

public class OrderViewModels
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public decimal? TotalAmount { get; set; }
    public DateTime? CreatedOn { get; set; }
    public byte OrderStatus { get; set; }

}
