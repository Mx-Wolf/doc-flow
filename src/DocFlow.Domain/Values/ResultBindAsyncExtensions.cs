namespace DocFlow.Domain.Values;

public static class ResultBindAsyncExtensions
{
    public static async Task<Result<T2, TError>> BindAsync<T1, T2, TError>(
        this Task<Result<T1, TError>> result,
        Func<T1, Result<T2, TError>> bindFunc) =>
        (await result).Bind(bindFunc);
    public static async Task<Result<T2, TError>> BindAsync<T1, T2, TError>(
        this Result<T1, TError> result,
        Func<T1, Task<Result<T2, TError>>> bindFunc) =>
        result.IsSuccess
            ? await bindFunc(result.Value)
            : Result<T2, TError>.Failure(result.Error);
    public static async Task<Result<T2, TError>> BindAsync<T1, T2, TError>(
        this Task<Result<T1, TError>> result,
        Func<T1, Task<Result<T2, TError>>> bindFunc) =>
        await (await result).BindAsync(bindFunc);
}