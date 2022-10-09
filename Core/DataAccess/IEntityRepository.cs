using System.Linq.Expressions;
using Core.Entities.Abstract;

namespace Core.DataAccess;

public interface IEntityRepository<T> where T : class, IEntity, new()
{
    T? Get(Expression<Func<T, bool>> predicate);
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    IList<T> GetList();
    Task<IList<T>> GetListAsync(CancellationToken cancellationToken);

    IList<T> GetList(Expression<Func<T, bool>> predicate);
    Task<IList<T>> GetListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    void Add(T entity);
    Task AddAsync(T entity, CancellationToken cancellationToken);

    void Update(T entity);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    void Delete(T entity);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}