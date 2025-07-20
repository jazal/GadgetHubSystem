namespace GadgetHub2.API.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }
    }
}