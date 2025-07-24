using AutoMapper;
using GadgetHub2.API.DTOs;
using GadgetHub2.API.DTOs.Quotations;
using GadgetHub2.API.Models;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationController : ControllerBase
{
    private readonly QuotationService _service;
    private readonly IMapper _mapper;

    public QuotationController(QuotationService quotationService, IMapper mapper)
    {
        _service = quotationService;
        _mapper = mapper;
    }

    [HttpPost("CreateQuotation")]
    public async Task<IActionResult> Create(CreateQuotationDto dto)
    {
        var quotation = _mapper.Map<Quotation>(dto);
        await _service.Add(quotation);
        return Ok(_mapper.Map<QuotationRequestDto>(quotation));
    }

    [HttpPost("getquotations")]
    public async Task<IActionResult> GetQuotations([FromBody] QuotationRequestDto request)
    {
        var quotations = await _service.GetQuotationsAsync(request);

        if (quotations == null || !quotations.Any())
        {
            return NotFound("No quotations available from distributors.");
        }

        return Ok(quotations);
    }
}
