using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.GenericRepository;
public interface IRepositoryReadAccess<TEntity, TKey>
    where TEntity : class
    where TKey : notnull
{
    Task<Result<TEntity, Exception>> FindAsync(TKey id, CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> QueryAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken);
    Task<IEnumerable<TResult>> QueryAsync<TResult>(ISpecification<TEntity, TResult> query, CancellationToken cancellationToken);
    Task<ISpecification<TEntity, TResult>> Bind<TResult>(Func<IQueryable<TEntity>, ISpecification<TEntity, TResult>> specification);
    Task<ISpecification<TEntity>> Bind(Func<IQueryable<TEntity>, ISpecification<TEntity>> specification);
}

/// <summary>
/// there are only synchronous methods in this interface.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TEntity, TKey>: IRepositoryReadAccess<TEntity, TKey>
    where TEntity : class
    where TKey : notnull
{
    Result<TEntity, Exception> Add(TEntity entity);
    Result<IReadOnlyCollection<TEntity>,Exception> AddRange(IReadOnlyCollection<TEntity> entities);
    Result<TEntity,Exception> Remove(TEntity entity);
    Result<IReadOnlyCollection<TEntity>, Exception> RemoveRange(IReadOnlyCollection<TEntity> entities);

}
