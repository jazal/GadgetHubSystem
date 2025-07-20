using GadgetHub2.API.Models;

namespace GadgetHub2.API.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User distributor);
    void Update(User distributor);
    void Delete(User distributor);
    Task SaveAsync();
}