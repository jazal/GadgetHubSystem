namespace GadgetHub2.API.Models;

public class Order
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalAmount { get; set; }

    public DateTime? CreatedOn { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public List<OrderItem> OrderItems { get; set; }

    public Order()
    {
        OrderItems = new List<OrderItem>();
    }
}

public enum OrderStatus : byte
{
    AssignToDistributor = 0,
    Completed = 1,
    Close = 2
}