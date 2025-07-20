using GadgetHub2.API.Base;
using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public class UserService
{
    private readonly IBaseRepository<User> _repo;

    public UserService(IBaseRepository<User> repo)
    {
        _repo = repo;
    }

    public Task<List<User>> GetAll() => _repo.GetAllAsync();
    public Task<User?> GetById(int id) => _repo.GetByIdAsync(id);
    public Task Add(User user) => _repo.AddAsync(user);
    public Task Update(User user) => _repo.UpdateAsync(user);
    public Task Delete(int id) => _repo.DeleteAsync(id);
}
