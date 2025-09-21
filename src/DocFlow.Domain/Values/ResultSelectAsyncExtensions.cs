namespace DocFlow.Domain.Values;

public static class ResultSelectAsyncExtensions
{
    // Async: Maps a successful Task<Result<T>> to Task<Result<U>>
    public static async Task<Result<U,E>> Select<T,E, U>(
        this Task<Result<T,E>> taskResult,
        Func<T, U> mapFunc)                               // Selector that maps T -> U
    {
        var result = await taskResult;                   // Await the original result
        return result.IsSuccess
            ? Result<U,E>.Success(mapFunc(result.Value))    // Map successful result
            : Result<U,E>.Failure(result.Error);     // Propagate failure
    }
}