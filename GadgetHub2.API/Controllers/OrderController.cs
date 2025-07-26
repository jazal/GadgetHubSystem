using GadgetHub.API.Distributors;
using GadgetHub.API.Repositories;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Distributors;
using GadgetHub.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ExternalApiService _externalApiService;

    public OrderController(OrderService orderService, ExternalApiService externalApiService)
    {
        _orderService = orderService;
        _externalApiService = externalApiService;
    }

    [HttpPost("PlaceOrder")]
    public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDto request)
    {
        var result = await _orderService.PlaceOrder(request);
        return Ok(result);
    }

    [HttpPost("GetAll")]
    public async Task<IActionResult> GetAll([FromBody] FilterOrderDto input)
    {
        var orders = await _orderService.GetOrders(input);

        if (input.IsDistributionQoutations == true) {

            var data = await _externalApiService.GetAllOrdersFromExternalApisAsync(new FilterQuotationDto { GadgetHubOrderId = input.OrderId });

        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _orderService.GetOrderById(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] OrderResponseDto dto)
    {
        var existingOrder = await _orderService.GetById(id);
        if (existingOrder == null)
            return NotFound();

        // Example: only updating TotalAmount
        //existingOrder.TotalAmount = dto.TotalAmount;

        await _orderService.Update(existingOrder);

        return Ok(existingOrder);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _orderService.GetById(id);
        if (order == null)
            return NotFound();

        await _orderService.Delete(id);
        return Ok();
    }

    [HttpGet("PendingOrders")]
    public async Task<IActionResult> GetPendingOrders()
    {
        var pendingOrders = await _orderService.GetPendingOrders();
        return Ok(pendingOrders);
    }
}
