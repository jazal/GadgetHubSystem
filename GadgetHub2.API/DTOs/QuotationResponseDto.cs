namespace GadgetHub2.API.DTOs
{
    public class QuotationResponseDto
    {
        public string DistributorName { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public int EstimatedDeliveryDays { get; set; }
    }
}