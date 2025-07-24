namespace GadgetHub.Dtos.Order;

public class UpdateOrderDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public decimal TotalAmount { get; set; }
}
