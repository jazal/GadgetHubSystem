namespace GadgetHub2.API.Base;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> GetAll();
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);

    //pending
    //Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    //Task<List<Order>> GetAllAsync();
}
