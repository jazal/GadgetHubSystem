namespace GadgetHub2.API.Models
{
    public class Quotation
    {
        public int Id { get; set; }

        public int DistributorId { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int EstimatedDeliveryDays { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
}