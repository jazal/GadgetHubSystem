namespace GadgetHub2.WEB.Models
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new();
    }
}

