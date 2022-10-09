using System.Linq.Expressions;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework;

public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new() where TContext : DbContext, new()
{
    public void Add(TEntity entity)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Added;
        context.SaveChanges();
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Added;
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Update(TEntity entity)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        using var context = new TContext();
        context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync(cancellationToken);
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
    {
        using var context = new TContext();
        return context.Set<TEntity>().SingleOrDefault(predicate);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public IList<TEntity> GetList()
    {
        using var context = new TContext();
        return context.Set<TEntity>().ToList();
    }

    public async Task<IList<TEntity>> GetListAsync(CancellationToken cancellationToken)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public IList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        using var context = new TContext();
        return context.Set<TEntity>().Where(predicate).ToList();
    }

    public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        using var context = new TContext();
        return await context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }
}