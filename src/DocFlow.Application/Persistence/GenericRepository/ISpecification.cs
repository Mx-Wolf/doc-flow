using System.Linq.Expressions;

namespace DocFlow.Application.Persistence.GenericRepository;

public interface ISpecification<TEntity>
    where TEntity : class
{
    Expression<Func<TEntity, bool>> ToExpression();
}
public interface ISpecification<TEntity, TResult>
    where TEntity : class
{
   IQueryable<TResult> Apply(IQueryable<TEntity> queryable);
}