using AutoMapper;
using GadgetHub.API.Distributors;
using GadgetHub.API.Repositories;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Quotations;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationsController : ControllerBase
{
    private readonly QuotationService _service;
    private readonly IMapper _mapper;
    private readonly ExternalApiService _externalApiService;

    public QuotationsController(
        QuotationService quotationService,
        IMapper mapper,
        ExternalApiService externalApiService)
    {
        _service = quotationService;
        _mapper = mapper;
        _externalApiService = externalApiService;
    }

    [HttpGet("GetAllByOrderId/{orderId}")]
    public async Task<IActionResult> GetAllByOrderId(int orderId)
    {
        var datas = await _externalApiService.GetAllOrdersFromExternalApisAsync(new FilterQuotationDto { GadgetHubOrderId = orderId });

        return Ok(datas);
    }

    #region Ne NEED REMOVE

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

    #endregion
}
