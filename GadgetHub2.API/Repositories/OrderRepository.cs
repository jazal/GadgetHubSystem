using GadgetHub2.API.Data;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class OrderRepository
{
    private readonly GadgetHubContext _context;

    public OrderRepository(GadgetHubContext context)
    {
        _context = context;
    }

    public List<Order> GetAll()
    {
        return _context.Orders.ToList();
    }

    public Order? GetById(int id)
    {
        return _context.Orders.Find(id);
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void Delete(Order order)
    {
        var items = _context.OrderItems.Where(x => x.OrderId == order.Id).ToList();
        _context.OrderItems.RemoveRange(items);

        _context.Orders.Remove(order);
        _context.SaveChanges();
    }
}

