using AutoMapper;
using GadgetHub2.API.DTOs;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(OrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public OrderResponseDto PlaceOrder(CreateOrderDto request)
    {
        var order = _mapper.Map<Order>(request);
        order.CreatedOn = DateTime.Now;

        _orderRepository.Add(order);

        return new OrderResponseDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
        };

        //// Calculate total amount
        //decimal total = request.Items.Sum(item => item.UnitPrice * item.Quantity);

        //// Create Order entity
        //var order = new Order
        //{
        //    CustomerId = request.CustomerId,
        //    TotalAmount = total
        //};

        //// Map OrderItems
        //var orderItems = request.Items.Select(item => new OrderItem
        //{
        //    ProductId = item.ProductId,
        //    Quantity = item.Quantity,
        //    Price = item.UnitPrice
        //}).ToList();

        //// Save order + items
        //_orderRepository.Add(order, orderItems);

        //// Prepare response
        //var response = new OrderResponseDto
        //{
        //    OrderId = order.Id,
        //    CustomerId = order.CustomerId,
        //    TotalAmount = order.TotalAmount ?? 0,
        //    EstimatedDeliveryDate = DateTime.Now.AddDays(5) // example delivery estimate
        //};

        //return response;
    }
}