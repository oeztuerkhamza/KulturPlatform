namespace KulturPlatform.Application.Common;

/// <summary>
/// Extension methods for Result types to simplify working with results
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Maps a Result<T> to Result<TNew> using a transformation function
    /// </summary>
    public static Result<TNew> Map<T, TNew>(this Result<T> result, Func<T, TNew> mapper)
    {
        if (result.IsFailure)
            return Result.Failure<TNew>(result.Error);

        return Result.Success(mapper(result.Value));
    }

    /// <summary>
    /// Chains an async operation to a Result<T>
    /// </summary>
    public static async Task<Result<TNew>> BindAsync<T, TNew>(
        this Result<T> result,
        Func<T, Task<Result<TNew>>> func)
    {
        if (result.IsFailure)
            return Result.Failure<TNew>(result.Error);

        return await func(result.Value);
    }

    /// <summary>
    /// Executes an action if the result is successful
    /// </summary>
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }

    /// <summary>
    /// Executes an action if the result is a failure
    /// </summary>
    public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
    {
        if (result.IsFailure)
            action(result.Error);

        return result;
    }

    /// <summary>
    /// Returns the value if successful, otherwise returns the default value
    /// </summary>
    public static T ValueOrDefault<T>(this Result<T> result, T defaultValue = default!)
    {
        return result.IsSuccess ? result.Value : defaultValue;
    }
}
