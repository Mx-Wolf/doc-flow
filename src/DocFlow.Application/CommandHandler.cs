using DocFlow.Domain.Values;

namespace DocFlow.Application;

public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    public abstract Task<Result<TResult, Exception>> HandleAsync(TCommand command, CancellationToken cancellationToken);
    // You can add common functionality for all command handlers here if needed
    protected Result<TResult, Exception> Success(TResult value)
    {
        return Result<TResult, Exception>.Success(value);
    }
    protected Result<TResult, Exception> Failure(Exception error)
    {
        return Result<TResult, Exception>.Failure(error);
    }
}