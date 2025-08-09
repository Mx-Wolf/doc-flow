using DocFlow.Domain.Values;

namespace DocFlow.Application;

public interface ICommandHandler<in TCommand, TResult>
{
    Task<Result<TResult, Exception>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}