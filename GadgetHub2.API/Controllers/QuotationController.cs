using AutoMapper;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Quotations;
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

    [HttpGet("GetByOrderId")]
    public async Task<IActionResult> GetByOrderId(int orderId)
    {
        var qoutations = _service.GetAll().Result
            .Where(x => x.OrderId == orderId)
            .Select(q => new QuotationDto
            {
                Id = q.Id,
                DistributorId = q.DistributorId,
                OrderId = q.OrderId,
                OrderItemId = q.OrderId,
                Price = q.Price,
                Quantity = q.Quantity,
                EstimatedDeliveryDays = q.EstimatedDeliveryDays,
                CreatedOn = q.CreatedOn
            }).ToList();

        return Ok(qoutations);
    }

    [HttpPost("CreateQuotation")]
    public async Task<IActionResult> Create(List<CreateQuotationDto> dto)
    {
        await _service.AddRange(dto);
        return Ok();
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
