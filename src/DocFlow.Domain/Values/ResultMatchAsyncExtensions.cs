namespace DocFlow.Domain.Values;

public static class ResultMatchAsyncExtensions
{
    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Task<Result<T, TError>> result,
        Func<T, TResult> onSuccess,
        Func<TError, TResult> onFailure) =>
        (await result).Match(onSuccess, onFailure);
    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Result<T, TError> result,
        Func<T, Task<TResult>> onSuccessAsync,
        Func<TError, TResult> onFailure) =>
        result.IsSuccess
            ? await onSuccessAsync(result.Value)
            : onFailure(result.Error);
    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Result<T, TError> result,
        Func<T, TResult> onSuccess,
        Func<TError, Task<TResult>> onFailureAsync) =>
        result.IsSuccess
            ? onSuccess(result.Value)
            : await onFailureAsync(result.Error);

    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Task<Result<T, TError>> result,
        Func<T, Task<TResult>> onSuccessAsync,
        Func<TError, TResult> onFailure) =>
        await (await result).MatchAsync(onSuccessAsync, onFailure);
    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Task<Result<T, TError>> result,
        Func<T, TResult> onSuccess,
        Func<TError, Task<TResult>> onFailureAsync) =>
        await (await result).MatchAsync(onSuccess, onFailureAsync);
    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Task<Result<T, TError>> result,
        Func<T, Task<TResult>> onSuccessAsync,
        Func<TError, Task<TResult>> onFailureAsync) =>
        await (await result).MatchAsync(onSuccessAsync, onFailureAsync);


    public static async Task<TResult> MatchAsync<T, TError, TResult>(
        this Result<T, TError> result,
        Func<T, Task<TResult>> onSuccess,
        Func<TError, Task<TResult>> onFailure) =>
        result.IsSuccess
            ? await onSuccess(result.Value)
            : await onFailure(result.Error);
}