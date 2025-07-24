using GadgetHub2.API.DTOs.Quotations;

namespace GadgetHub2.API.DTOs.OrderItems;

public class OrderItemDto
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}
