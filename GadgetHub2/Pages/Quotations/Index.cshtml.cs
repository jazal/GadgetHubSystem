using GadgetHub2.WEB.Models;
using GadgetHub2.WEB.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GadgetHub2.WEB.Pages.Quotations;

public class IndexModel : PageModel
{
    private readonly QuotationService _quotationService;

    public IndexModel(QuotationService quotationService)
    {
        _quotationService = quotationService;
    }

    [BindProperty(SupportsGet = true)]
    public int ProductId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Quantity { get; set; } = 1;

    public List<QuotationResponse> Quotations { get; set; } = new();

    public async Task OnGetAsync()
    {
        var request = new QuotationRequest
        {
            ProductId = ProductId,
            Quantity = Quantity
        };

        Quotations = await _quotationService.GetQuotationsAsync(request);
    }
}
