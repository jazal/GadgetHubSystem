using AutoMapper;
using GadgetHub.API.Base;
using GadgetHub.API.Models;
using GadgetHub.Dtos;
using GadgetHub.Dtos.Enums;
using GadgetHub.Dtos.Order;
using GadgetHub.Dtos.OrderItems;
using GadgetHub.Dtos.Quotations;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Repositories;

public class OrderService
{
    private readonly IBaseRepository<Order> _repo;
    private readonly IBaseRepository<User> _userRepo;
    private readonly IBaseRepository<Quotation> _quoRepo;
    private readonly IMapper _mapper;

    public OrderService(
        IBaseRepository<Order> repo,
        IBaseRepository<User> userRepo,
        IBaseRepository<Quotation> quoRepo,
        IMapper mapper)
    {
        _repo = repo;
        _userRepo = userRepo;
        _quoRepo = quoRepo;
        _mapper = mapper;
    }

    public Task<List<Order>> GetAll() => _repo.GetAllAsync();
    public Task<Order?> GetById(int id) => _repo.GetByIdAsync(id);
    public Task Add(Order order) => _repo.AddAsync(order);
    public Task Update(Order order) => _repo.UpdateAsync(order);
    public Task Delete(int id) => _repo.DeleteAsync(id);

    public async Task<OrderResponseDto> PlaceOrder(CreateOrderDto request)
    {
        var order = _mapper.Map<Order>(request);
        order.CreatedOn = DateTime.Now;
        // order.TotalAmount ??= 0m;

        await _repo.AddAsync(order);

        return new OrderResponseDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
        };
    }

    public async Task<List<CustomerOrderDto>> GetPendingOrders()
    {
        var orders = await (
            from order in _repo.GetAll()
            join user in _userRepo.GetAll() on order.CustomerId equals user.Id
            where order.OrderStatus == OrderStatus.AssignToDistributor
            select new CustomerOrderDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
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
            from order in _repo.GetAll()
            join user in _userRepo.GetAll() on order.CustomerId equals user.Id

            //join q in _quoRepo.GetAll() on order.Id equals q.OrderId into quotationGroup
            //from quotation in quotationGroup.DefaultIfEmpty() // left join

            join q in _quoRepo.GetAll() on order.Id equals q.OrderId into quotationGroup

            where order.Id == orderId
            select new CustomerOrderDto
            {
                OrderId = order.Id,
                TotalAmount = order.TotalAmount,
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
                }).ToList(),
                Quotations = quotationGroup.Where(q => q != null)
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
                }).ToList()
            }
        ).ToListAsync();

        return orders;
    }
}