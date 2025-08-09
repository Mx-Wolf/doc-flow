namespace DocFlow.Domain.Values;

public static class ResultExtensions
{
    public static Result<T2,TError> Map<T1, T2, TError>(
        this Result<T1, TError> result,
        Func<T1, T2> mapFunc) =>
        result.IsSuccess 
            ? Result<T2, TError>.Success(mapFunc(result.Value)) 
            : Result<T2, TError>.Failure(result.Error);
    public static Result<T2, TError> Bind<T1, T2, TError>
        (this Result<T1, TError> result,
        Func<T1, Result<T2, TError>> bindFunc) =>
        result.IsSuccess 
            ? bindFunc(result.Value) 
            : Result<T2, TError>.Failure(result.Error);
    public static Result<T, TError2> MapError<T, TError, TError2>(
        this Result<T, TError> result,
        Func<TError, TError2> mapErrorFunc) =>
        result.IsSuccess 
            ? Result<T, TError2>.Success(result.Value) 
            : Result<T, TError2>.Failure(mapErrorFunc(result.Error));
    public static TResult Match<T, TError, TResult>(
        this Result<T, TError> result,
        Func<T, TResult> onSuccess,
        Func<TError, TResult> onFailure) =>
        result.IsSuccess 
            ? onSuccess(result.Value) 
            : onFailure(result.Error);
}