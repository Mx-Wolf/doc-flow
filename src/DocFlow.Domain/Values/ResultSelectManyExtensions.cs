namespace DocFlow.Domain.Values;

public static class ResultSelectManyExtensions
{
    // Maps and flattens the Result<T> -> Result<U>
    public static Result<U,E> SelectMany<T,E, U>(
        this Result<T,E> result,
        Func<T, Result<U,E>> bindFunc)                      // Function that maps T -> Result<U>
    {
        if (!result.IsSuccess)
            return Result<U,E>.Failure(result.Error); // Propagate failure
        return bindFunc(result.Value);                    // Bind into the next computation if successful
    }

    // Overload for projection combining results
    public static Result<V,E> SelectMany<T,E, U, V>(
        this Result<T,E> result,
        Func<T, Result<U,E>> bindFunc,                      // Function mapping T -> Result<U>
        Func<T, U, V> projectFunc)                        // Projecting function T, U -> V
    {
        if (!result.IsSuccess)
            return Result<V,E>.Failure(result.Error); // Propagate failure

        var intermediate = bindFunc(result.Value);
        if (!intermediate.IsSuccess)
            return Result<V,E>.Failure(intermediate.Error); // Propagate inner failure

        return Result<V,E>.Success(projectFunc(result.Value, intermediate.Value)); // Combine both results
    }
}