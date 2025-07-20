using GadgetHub2.API.Models;
using GadgetHub2.API.Data;

namespace GadgetHub2.API.Repositories;

public class ProductRepository
{
    private readonly GadgetHubContext _context;

    public ProductRepository(GadgetHubContext context)
    {
        _context = context;
    }

    public List<Product> GetAll()
    {
        return _context.Products.ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products.Find(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        var existingProduct = _context.Products.Find(product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            _context.SaveChanges();
        }
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
