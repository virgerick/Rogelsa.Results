using Rogelsa.Results.Failures;
using Rogelsa.Results.Succeed;

namespace Rogelsa.Results;

//Rogelsoft
/// <summary>
///     Represent an result value
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class Result<TValue>
{
    private readonly Error? _error;
    private readonly Success<TValue>? _success;

    /// <summary>
    ///     Constructor for result type
    /// </summary>
    /// <param name="success">Represent a success value result </param>
    /// <param name="error">Represent an error value result</param>
    /// <exception cref="InvalidOperationException">Occur when there is no given Success or Error</exception>
    protected Result(Success<TValue>? success, Error? error)
    {
        if (success is null && error is null) throw new InvalidOperationException("No success or error given");
        _success = success;
        _error = error;
    }

    /// <summary>
    ///     Create a Success result
    /// </summary>
    /// <param name="success"></param>
    /// <returns></returns>
    public static Result<TValue> Success(Success<TValue> success)
    {
        return new Result<TValue>(success, null);
    }

    /// <summary>
    ///     Create an Error result
    /// </summary>
    /// <param name="error"></param>
    /// <returns></returns>
    public static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(null, error);
    }

    public Result<TValue> OnSuccess(Action<Success<TValue>> onSuccess)
    {
        if (_success is not null) onSuccess.Invoke(_success!);
        return this;
    }

    public Result<TValue> OnFailure(Action<Error> onError)
    {
        if (_error is not null) onError.Invoke(_error!);

        return this;
    }

    public TResult Switch<TResult>(Func<Success<TValue>, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return _success is not null ? onSuccess.Invoke(_success!) : onFailure.Invoke(_error!);
    }
}

/// <summary>
///     Represent a result with no value
/// </summary>
public sealed class Result : Result<None>
{
    private Result(Success<None>? success, Error? error) : base(success, error)
    {
    }

    public new static Result Success(Success<None> success)
    {
        return new Result(success, null);
    }

    public new static Result Failure(Error error)
    {
        return new Result(null, error);
    }
}