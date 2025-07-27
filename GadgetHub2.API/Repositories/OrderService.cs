using AutoMapper;
using GadgetHub.API.Data;
using GadgetHub.API.Extensions;
using GadgetHub.API.Models;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Enums;
using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.OrderItems;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Repositories;

public class OrderService
{
    private readonly GadgetHubContext _context;
    private readonly IMapper _mapper;

    public OrderService(
        GadgetHubContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<OrderResponseDto> PlaceOrder(CreateOrderDto request)
    {
        var order = _mapper.Map<Order>(request);
        order.CreatedOn = DateTime.Now;
        order.TotalAmount = request.OrderItems.Select(x => x.Price * x.Quantity).Sum();

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return new OrderResponseDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
        };
    }

    public async Task<List<CustomerOrderDto>> GetOrders(FilterOrderDto input)
    {
        var orders = await (
            from order in _context.Orders.Include(x => x.OrderItems)
                .WhereIf(input.OrderId.HasValue, x => x.Id == input.OrderId)
                .WhereIf(input.CustomerId.HasValue, x => x.CustomerId == input.CustomerId)
                .WhereIf(input.OrderStatus.HasValue, x => x.OrderStatus == input.OrderStatus)

            join user in _context.Users on order.CustomerId equals user.Id

            select new CustomerOrderDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,

                QuotationId = order.QuotationId,
                DistributorName = order.DistributorName,
                ApiUrl = order.ApiUrl,
                AssignedOn = order.AssignedOn,

                ConfirmedOn = order.ConfirmedOn,

                CreatedOn = order.CreatedOn,
                OrderStatus = order.OrderStatus,
                CustomerId = user.Id,
                CustomerName = user.Name,
                CustomerEmail = user.Email,
                OrderItems = order.OrderItems.Select(ot => new OrderItemDto
                {
                    Id = ot.Id,
                    OrderId = ot.Id,
                    Price = ot.Price,
                    ProductId = ot.ProductId,
                    ProductName = ot.ProductName,
                    Quantity = ot.Quantity
                }).ToList()
            }
        ).ToListAsync();

        return orders;
    }

    public async Task<List<CustomerOrderDto>> GetPendingOrders()
    {
        var orders = await (
            from order in _context.Orders.Include(x => x.OrderItems)
            join user in _context.Users on order.CustomerId equals user.Id
            where order.OrderStatus == OrderStatus.AssignToDistributor
            select new CustomerOrderDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,

                QuotationId = order.QuotationId,
                DistributorName = order.DistributorName,
                ApiUrl = order.ApiUrl,
                AssignedOn = order.AssignedOn,

                ConfirmedOn = order.ConfirmedOn,

                CreatedOn = order.CreatedOn,
                OrderStatus = order.OrderStatus,
                CustomerId = user.Id,
                CustomerName = user.Name,
                CustomerEmail = user.Email
            }
        ).ToListAsync();
        
        return orders;
    }

    public async Task<List<CustomerOrderDto>> GetOrderById(int orderId)
    {
        var orders = await (
            from order in _context.Orders.Include(x => x.OrderItems)
            join user in _context.Users on order.CustomerId equals user.Id

            where order.Id == orderId
            select new CustomerOrderDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,

                QuotationId = order.QuotationId,
                DistributorName = order.DistributorName,
                ApiUrl = order.ApiUrl,
                AssignedOn = order.AssignedOn,

                ConfirmedOn = order.ConfirmedOn,

                CreatedOn = order.CreatedOn,
                OrderStatus = order.OrderStatus,
                CustomerId = user.Id,
                CustomerName = user.Name,
                CustomerEmail = user.Email,
                OrderItems = order.OrderItems.Select(ot => new OrderItemDto
                {
                    Id = ot.Id,
                    OrderId = ot.Id,
                    Price = ot.Price,
                    ProductId = ot.ProductId,
                    ProductName = ot.ProductName,
                    Quantity = ot.Quantity
                }).ToList()
            }
        ).ToListAsync();

        return orders;
    }
}