using GadgetHub2.API.Data;
using GadgetHub2.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub2.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly GadgetHubContext _context;

    public UserRepository(GadgetHubContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User distributor)
    {
        await _context.Users.AddAsync(distributor);
    }

    public void Update(User distributor)
    {
        _context.Users.Update(distributor);
    }

    public void Delete(User distributor)
    {
        _context.Users.Remove(distributor);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
