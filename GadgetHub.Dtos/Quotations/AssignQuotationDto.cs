namespace GadgetHub.Dtos.Quotations;

public class AssignQuotationDto
{
    public int OrderId { get; set; }

    public int QuotationId { get; set; }

    public string? ApiUrlId { get; set; }

    public string? DistributorName { get; set; }

    public List<ConfirmedQuotationItemDto> Items { get; set; }

    public AssignQuotationDto()
    {
        Items = new List<ConfirmedQuotationItemDto>();
    }
}

public class ConfirmedQuotationItemDto
{
    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}

