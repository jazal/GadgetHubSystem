using GadgetHub2.API.Base;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class ProductService
{
    private readonly IBaseRepository<Product> _repo;

    public ProductService(IBaseRepository<Product> repo)
    {
        _repo = repo;
    }

    public Task<List<Product>> GetAll() => _repo.GetAllAsync();
    public Task<Product?> GetById(int id) => _repo.GetByIdAsync(id);
    public Task Add(Product product) => _repo.AddAsync(product);
    public Task Update(Product product) => _repo.UpdateAsync(product);
    public Task Delete(int id) => _repo.DeleteAsync(id);
}

