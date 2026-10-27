using System.Linq.Expressions;
using Lab10_MaxiFranco.Domain.Interfaces.Repositories;
using Lab10_MaxiFranco.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10_MaxiFranco.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class
{
    protected readonly TicketeraDbContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(TicketeraDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(params object[] keys)
    {
        return await DbSet.FindAsync(keys);
    }

    public async Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate)
    {
        return await DbSet
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        DbSet.Remove(entity);
    }
}