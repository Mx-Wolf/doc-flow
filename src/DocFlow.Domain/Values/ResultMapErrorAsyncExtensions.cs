namespace DocFlow.Domain.Values;

public static class ResultMapErrorAsyncExtensions
{
    public static async Task<Result<T, TError2>> MapErrorAsync<T, TError, TError2>(
        this Task<Result<T, TError>> result,
        Func<TError, TError2> mapErrorFunc) =>
        (await result).MapError(mapErrorFunc);
    public static async Task<Result<T, TError2>> MapErrorAsync<T, TError, TError2>(
        this Result<T, TError> result,
        Func<TError, Task<TError2>> mapErrorFunc) =>
        result.IsSuccess
            ? Result<T, TError2>.Success(result.Value)
            : Result<T, TError2>.Failure(await mapErrorFunc(result.Error));
    public static async Task<Result<T, TError2>> MapErrorAsync<T, TError, TError2>(
        this Task<Result<T, TError>> result,
        Func<TError, Task<TError2>> mapErrorFunc) =>
        await (await result).MapErrorAsync(mapErrorFunc);
}