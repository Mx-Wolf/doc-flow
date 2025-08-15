using DocFlow.Domain.Values;

namespace DocFlow.Application.Persistence.GenericRepository;

public interface IUnitOfWork
{
    Task<Result<int, Exception>> SaveChangesAsync(CancellationToken cancellationToken);
}

public static class UnitOfWorkExtensions
{
    public static async Task<Result<TResult, Exception>> SaveChangesAsync<TResult>(this IUnitOfWork unitOfWork, TResult result, CancellationToken cancellationToken = default)
    {
        return await unitOfWork.SaveChangesAsync(cancellationToken).MapAsync(_=>result);
    }
}