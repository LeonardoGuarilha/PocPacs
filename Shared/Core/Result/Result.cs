namespace Shared.Core.Result;

public class Result<TValue>
{
    private readonly TValue? _value;
    private readonly Error _error;

    public readonly bool IsSuccess;

    public Result(TValue value)
    {
        IsSuccess = true;
        _value = value;
        _error = Error.None;
    }

    public Result(Error error)
    {
        IsSuccess = false;
        _value = default;
        _error = error;
    }

    public static Result<TValue> Success(TValue value) => new(value);
    public static Result<TValue> Failure(Error error) => new(error);

    // Implicit operators
    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);


    // Methods
    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<Error, TResult> failure)
    {
        return IsSuccess ? success(_value!) : failure(_error);
    }
}
