namespace DocFlow.Domain.Values;

public static class ResultSelectManyAsyncExtensions
{
    // Async: Maps and flattens Task<Result<T>> -> Task<Result<U>>
    public static async Task<Result<U,E>> SelectMany<T,E, U>(
        this Task<Result<T,E>> taskResult,
        Func<T, Task<Result<U,E>>> bindFunc)               // Function mapping T -> Task<Result<U>>
    {
        var result = await taskResult;                   // Await the original result
        if (!result.IsSuccess)
            return Result<U,E>.Failure(result.Error); // Propagate failure

        return await bindFunc(result.Value);             // Bind into the next async computation
    }

    // Async SelectMany overload for projection combining results
    public static async Task<Result<V,E>> SelectMany<T,E, U, V>(
        this Task<Result<T,E>> taskResult,
        Func<T, Task<Result<U,E>>> bindFunc,               // Async binding T -> Task<Result<U>>
        Func<T, U, V> projectFunc)                       // Projecting function T, U -> V
    {
        var result = await taskResult;                   // Await the original result
        if (!result.IsSuccess)
            return Result<V,E>.Failure(result.Error); // Propagate failure

        var intermediate = await bindFunc(result.Value);  // Bind and await the next computation
        if (!intermediate.IsSuccess)
            return Result<V,E>.Failure(intermediate.Error); // Propagate inner failure

        return Result<V,E>.Success(projectFunc(result.Value, intermediate.Value)); // Combine both results
    }
}