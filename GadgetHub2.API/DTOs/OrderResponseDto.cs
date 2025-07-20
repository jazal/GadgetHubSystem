namespace GadgetHub2.API.DTOs
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
    }
}