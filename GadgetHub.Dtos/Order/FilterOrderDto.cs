using GadgetHub.Dtos.Enums;

namespace GadgetHub.Dtos.Order;

public class FilterOrderDto
{
    public int? OrderId { get; set; }

    public int? CustomerId { get; set; }

    public OrderStatus? OrderStatus { get; set; }
}
