using Rogelsa.Results.Failures;
using Rogelsa.Results.Succeed;

namespace Rogelsa.Results;

/// <summary>
/// Represents the result of an operation that can either succeed with a value of type <typeparamref name="TValue"/> or fail with an <see cref="Error"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value returned in case of success.</typeparam>
public  class  Result<TValue>
{
    /// <summary>
    /// Gets the error value if the operation failed; otherwise, <c>null</c>.
    /// </summary>
    private readonly Error? _error;
    
    /// <summary>
    /// Gets the success value if the operation succeeded; otherwise, <c>null</c>.
    /// </summary>
    private readonly Success<TValue>? _success;
    
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    private readonly bool _isSuccess;
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with a success value.
    /// </summary>
    /// <param name="success">The success value to initialize the result with.</param>
    protected Result(Success<TValue> success)
    {
        _success = success;
        _isSuccess = true;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{TValue}"/> class with an error value.
    /// </summary>
    /// <param name="error">The error that caused the operation to fail.</param>
    protected Result(Error error)
    {
        _error = error;
        _isSuccess = false;
    }
    /// <summary>
    /// Creates a new successful result with the specified success value.
    /// </summary>
    /// <param name="success">The success value containing the operation result.</param>
    /// <returns>A new <see cref="Result{TValue}"/> representing a successful operation.</returns>
    public static Result<TValue> Success(Success<TValue> success)
    {
        return new Result<TValue>(success);
    }

    /// <summary>
    /// Creates a new failed result with the specified error.
    /// </summary>
    /// <param name="error">The error that caused the operation to fail.</param>
    /// <returns>A new <see cref="Result{TValue}"/> representing a failed operation.</returns>
    public static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(error);
    }

    /// <summary>
    /// Executes the specified action if the result represents a success.
    /// </summary>
    /// <param name="onSuccess">The action to execute with the success value.</param>
    /// <returns>The current result instance to allow method chaining.</returns>
    public Result<TValue> OnSuccess(Action<Success<TValue>> onSuccess)
    {
        if (_isSuccess){
            onSuccess.Invoke(_success!);
        } 
            
        return this;
    }

    /// <summary>
    /// Executes the specified action if the result represents a failure.
    /// </summary>
    /// <param name="onFailure">The action to execute with the error value.</param>
    /// <returns>The current result instance to allow method chaining.</returns>
    public Result<TValue> OnFailure(Action<Error> onFailure)
    {
        if (!_isSuccess) 
        {
            onFailure.Invoke(_error!);
        }
        return this;
    }

    /// <summary>
    /// Executes one of the provided functions based on whether the result is a success or failure.
    /// </summary>
    /// <typeparam name="TResult">The type of the value to return.</typeparam>
    /// <param name="onSuccess">The function to execute if the result is a success.</param>
    /// <param name="onFailure">The function to execute if the result is a failure.</param>
    /// <returns>The result of the executed function, which depends on the success or failure state.</returns>
    public TResult Switch<TResult>(Func<Success<TValue>, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return _isSuccess ? onSuccess.Invoke(_success!) : onFailure.Invoke(_error!);
    }
    /// <summary>
    /// Transforms the success value using the specified mapping function if the result is a success.
    /// </summary>
    /// <typeparam name="TNewValue">The type of the value after transformation.</typeparam>
    /// <param name="onMap">The function that transforms the success value.</param>
    /// <returns>
    /// A new <see cref="Result{TNewValue}"/> with the transformed value if the operation was successful;
    /// otherwise, returns the original error.
    /// </returns>
    public Result<TNewValue> Map<TNewValue>(Func<Success<TValue>, Success<TNewValue>> onMap)
    {
        return _isSuccess 
            ? onMap.Invoke(_success!)
            : _error!;
    }
    
    /// <summary>
    /// Implicitly converts a <see cref="Success{TValue}"/> to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="success">The success value to convert.</param>
    /// <returns>A new <see cref="Result{TValue}"/> representing a successful operation.</returns>
    public static implicit operator Result<TValue>(Success<TValue> success) => Success(success);
    
    /// <summary>
    /// Implicitly converts an <see cref="Error"/> to a <see cref="Result{TValue}"/>.
    /// </summary>
    /// <param name="error">The error that caused the operation to fail.</param>
    /// <returns>A new <see cref="Result{TValue}"/> representing a failed operation.</returns>
    public static implicit operator Result<TValue>(Error error) => Failure(error);
}


/// <summary>
/// Represents the result of an operation that doesn't return a value but can still fail with an <see cref="Error"/>.
/// This is a non-generic version of <see cref="Result{TValue}"/> where the success case doesn't carry any value.
/// </summary>
public  class  Result : Result<None>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with a success value.
    /// </summary>
    /// <param name="success">The success value representing a successful operation with no return value.</param>
    private Result(Success<None> success) : base(success) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with an error value.
    /// </summary>
    /// <param name="error">The error that caused the operation to fail.</param>
    private Result(Error error) : base(error) { }
    /// <summary>
    /// Creates a new successful result with no value.
    /// </summary>
    /// <param name="success">The success value representing a successful operation with no return value.</param>
    /// <returns>A new <see cref="Result"/> representing a successful operation.</returns>
    public new static Result Success(Success<None> success) => new Result(success);
    
    /// <summary>
    /// Creates a new failed result with the specified error.
    /// </summary>
    /// <param name="error">The error that caused the operation to fail.</param>
    /// <returns>A new <see cref="Result"/> representing a failed operation.</returns>
    public new static Result Failure(Error error) => new Result(error);
    
    
}