using System.Linq.Expressions;

namespace DemoApp.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task<T?> GetByIdAsync(Guid id);
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
}