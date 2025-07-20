using GadgetHub2.API.DTOs;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("PlaceOrder")]
    public IActionResult PlaceOrder([FromBody] CreateOrderDto request)
    {
        var result = _orderService.PlaceOrder(request);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var orders = _orderRepository.GetAll();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var order = _orderRepository.GetById(id);
        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] OrderResponseDto dto)
    {
        var existingOrder = _orderRepository.GetById(id);
        if (existingOrder == null)
            return NotFound();

        // Example: only updating TotalAmount
        existingOrder.TotalAmount = dto.TotalAmount;

        _orderRepository.Update(existingOrder);

        return Ok(existingOrder);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var order = _orderRepository.GetById(id);
        if (order == null)
            return NotFound();

        _orderRepository.Delete(order);
        return Ok();
    }
}
