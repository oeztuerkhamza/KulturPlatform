namespace KulturPlatform.Application.Common;

/// <summary>
/// Represents the result of an operation that can succeed or fail
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A successful result cannot have an error.");
        if (!isSuccess && string.IsNullOrEmpty(error))
            throw new InvalidOperationException("A failed result must have an error message.");

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Creates a successful result
    /// </summary>
    public static Result Success() => new(true, string.Empty);

    /// <summary>
    /// Creates a failed result with an error message
    /// </summary>
    public static Result Failure(string error) => new(false, error);

    /// <summary>
    /// Creates a successful result with a value
    /// </summary>
    public static Result<T> Success<T>(T value) => new(value, true, string.Empty);

    /// <summary>
    /// Creates a failed result with an error message
    /// </summary>
    public static Result<T> Failure<T>(string error) => new(default!, false, error);
}

/// <summary>
/// Represents the result of an operation that returns a value
/// </summary>
public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    /// <summary>
    /// Implicitly converts a value to a successful result
    /// </summary>
    public static implicit operator Result<T>(T value) => Success(value);
}
