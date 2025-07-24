namespace GadgetHub2.WEB.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime? CreatedOn { get; set; }
        public decimal TotalAmount { get; set; }
        public byte OrderStatus { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
    

