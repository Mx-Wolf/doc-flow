namespace DocFlow.Domain.Values;
public static class ResultSelectExtensions
{
    // Maps a successful Result<T> to Result<U>
    public static Result<U,E> Select<T, E, U>(
        this Result<T,E> result,
        Func<T, U> mapFunc)                               // Selector that maps T -> U
    {
        if (!result.IsSuccess)
            return Result<U,E>.Failure(result.Error); // Propagate the failure
        return Result<U,E>.Success(mapFunc(result.Value));   // Apply the mapping if successful
    }
}
