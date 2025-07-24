namespace GadgetHub.Web.Models
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}

