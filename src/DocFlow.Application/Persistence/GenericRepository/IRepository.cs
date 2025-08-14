using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.GenericRepository;
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : notnull
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task<Result<TEntity,Exception>> FindAsync(TKey id, CancellationToken cancellationToken);
    Task<IQueryable<TEntity>> QueryAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    Task<IQueryable<TResult>> QueryAsync<TResult>(ISpecification<TEntity, TResult> query, CancellationToken cancellationToken);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

}
