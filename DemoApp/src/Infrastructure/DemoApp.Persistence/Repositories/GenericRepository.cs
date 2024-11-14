using System.Linq.Expressions;
using DemoApp.Application.Interfaces.Repositories;
using DemoApp.Domain.Entities;
using DemoApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Persistence.Repositories;

public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        var record = await context.Set<T>().FindAsync(entity.Id);
        if (record is not null)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().Where(predicate).ToListAsync();
    }
}