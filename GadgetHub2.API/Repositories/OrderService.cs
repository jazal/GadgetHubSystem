using AutoMapper;
using GadgetHub2.API.Base;
using GadgetHub2.API.DTOs;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class OrderService
{
    private readonly IBaseRepository<Order> _repo;
    private readonly IMapper _mapper;

    public OrderService(IBaseRepository<Order> repo, IMapper mapper)
    {
        _repo = repo;
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

        await _repo.AddAsync(order);

        return new OrderResponseDto
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId,
        };
    }
}