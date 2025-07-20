using GadgetHub2.API.DTOs;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationController : ControllerBase
{
    private readonly QuotationService _quotationService;

    public QuotationController(QuotationService quotationService)
    {
        _quotationService = quotationService;
    }

    [HttpPost("get-quotations")]
    public async Task<IActionResult> GetQuotations([FromBody] QuotationRequestDto request)
    {
        var quotations = await _quotationService.GetQuotationsAsync(request);

        if (quotations == null || !quotations.Any())
        {
            return NotFound("No quotations available from distributors.");
        }

        return Ok(quotations);
    }
}
