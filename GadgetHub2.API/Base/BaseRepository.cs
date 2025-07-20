using GadgetHub2.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub2.API.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly GadgetHubContext _context;
    private readonly DbSet<T> _set;

    public BaseRepository(GadgetHubContext context)
    {
        _context = context;
        _set = _context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync() => await _set.ToListAsync();

    public async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        _set.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _set.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        if (entity != null)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
