namespace DocFlow.Domain.Values;

public class Result<T, TError>
{
    private readonly T? _value;
    private readonly TError? _error;
    private Result(T? value, TError? error)
    {
        _value = value;
        _error = error;
    }
    public static Result<T, TError> Success(T value)
    {
        return new Result<T, TError>(value, default);
    }
    public static Result<T, TError> Failure(TError error)
    {
        return new Result<T, TError>(default, error);
    }

    public bool IsSuccess => _error is null && _value is not null;

    public T Value
    {
        get =>
            IsSuccess
                ? _value!
                : throw new InvalidOperationException("Cannot access Value when Result is not successful.");
    }
    public TError Error
    {
        get =>
            IsSuccess
                ? throw new InvalidOperationException("Cannot access Error when Result is successful.")
                : _error!;
    }
}