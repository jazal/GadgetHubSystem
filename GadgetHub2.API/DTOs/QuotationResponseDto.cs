namespace GadgetHub2.API.DTOs
{
    public class QuotationResponseDto
    {
        public string DistributorName { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int EstimatedDeliveryDays { get; set; }
    }
}