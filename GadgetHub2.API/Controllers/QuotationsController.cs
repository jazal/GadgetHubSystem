using AutoMapper;
using GadgetHub.API.Data;
using GadgetHub.API.Distributors;
using GadgetHub.API.Repositories;
using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Quotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuotationsController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly IMapper _mapper;
    private readonly ExternalApiService _externalApiService;
    private readonly GadgetHubContext _context;

    public QuotationsController(
        OrderService orderService,
        IMapper mapper,
        ExternalApiService externalApiService,
        GadgetHubContext context
        )
    {
        _orderService = orderService;
        _mapper = mapper;
        _externalApiService = externalApiService;
        _context = context;
    }

    [HttpGet("GetAllByOrderId/{orderId}")]
    public async Task<IActionResult> GetAllByOrderId(int orderId)
    {
        var datas = await _externalApiService.GetAllOrdersFromExternalApisAsync(new FilterQuotationDto { GadgetHubOrderId = orderId });

        return Ok(datas);
    }

    [HttpPost("AssignQuotations")]
    public async Task<IActionResult> Create(AssignQuotationDto input)
    {
        // 1. Change status in Order 
        var order = await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == input.OrderId);

        if (order == null) return NotFound("Order not found!");

        order.OrderStatus = Dtos.Enums.OrderStatus.Completed;
        order.QuotationId = input.QuotationId;
        order.DistributorName = input.DistributorName;
        order.ApiUrl = input.ApiUrlId;
        order.AssignedOn = DateTime.Now;

        foreach (var item in input.Items)
        {
            var orderItem = order.OrderItems.FirstOrDefault(x => x.ProductName == item.ProductName);

            if (orderItem is not null)
            {
                orderItem.Price = item.Price;
                orderItem.Quantity = item.Quantity;
            }
        }

        order.TotalAmount = order.OrderItems.Select(x => x.Quantity * x.Price).Sum();

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        // 2. Change status in Dis Qoutations 
        // TODO


        return Ok();
    }

}
