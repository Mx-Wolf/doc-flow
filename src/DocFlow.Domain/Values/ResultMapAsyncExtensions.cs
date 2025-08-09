namespace DocFlow.Domain.Values;

public static class ResultMapAsyncExtensions
{
    public static async Task<Result<T2, TError>> MapAsync<T1, T2, TError>(
        this Task<Result<T1, TError>> result,
        Func<T1, T2> mapFunc) =>
        (await result).Map(mapFunc);
    public static async Task<Result<T2, TError>> MapAsync<T1, T2, TError>(
        this Result<T1, TError> result,
        Func<T1, Task<T2>> mapFunc) =>
        result.IsSuccess
            ? Result<T2, TError>.Success(await mapFunc(result.Value))
            : Result<T2, TError>.Failure(result.Error);

    public static async Task<Result<T2, TError>> MapAsync<T1, T2, TError>(
        this Task<Result<T1, TError>> result,
        Func<T1, Task<T2>> mapFunc) =>
        await (await result).MapAsync(mapFunc);

}