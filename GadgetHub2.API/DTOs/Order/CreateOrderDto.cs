namespace GadgetHub2.API.DTOs;

public class CreateOrderDto
{
    public int? CustomerId { get; set; }

    public List<CreateOrderItemDto> OrderItems { get; set; }

    public CreateOrderDto()
    {
        OrderItems = new List<CreateOrderItemDto>();
    }
}

public class CreateOrderItemDto
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}