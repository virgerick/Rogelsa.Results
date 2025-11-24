using Rogelsa.Results.Failures;
using Rogelsa.Results.Succeed;
using System.Diagnostics.CodeAnalysis;

namespace Rogelsa.Results.Extensions;

/// <summary>
/// Provides functional programming extensions for working with <see cref="Result{T}"/> and <see cref="Result"/> types.
/// </summary>
public static class FunctionalExtensions
{


    /// <summary>
    /// Asynchronously chains a result-returning function to a result.
    /// </summary>
    /// <typeparam name="TIn">The type of the input result's success value.</typeparam>
    /// <typeparam name="TOut">The type of the output result's success value.</typeparam>
    /// <param name="resultTask">A task that represents the asynchronous operation to get the result to bind.</param>
    /// <param name="bind">The asynchronous function that takes the success value and returns a new result.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the bound result.</returns>
    /// <remarks>
    /// This method is used to chain asynchronous operations that return Results. If the input result is a success,
    /// the provided function is called with the success value. If the input result is an error, the error is propagated.
    /// </remarks>
    public static async Task<Result<TOut>> BindAsync<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<Success<TIn>, Task<Result<TOut>>> bind)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SwitchAsync(
            async success => await bind(success).ConfigureAwait(false),
            error => Task.FromResult<Result<TOut>>(error)
        ).ConfigureAwait(false);
    }

    


    /// <summary>
    /// Handles both success and error cases by applying the appropriate function.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="result">The result to match on.</param>
    /// <param name="onSuccess">The function to call if the result is successful.</param>
    /// <param name="onError">The function to call if the result is an error.</param>
    /// <returns>The result of applying the appropriate function.</returns>
    /// <remarks>
    /// This method provides a way to handle both success and error cases in a single expression.
    /// It's similar to pattern matching on the result type.
    /// </remarks>
    public static TResult Match<T, TResult>(
        this Result<T> result,
        Func<Success<T>, TResult> onSuccess,
        Func<Error, TResult> onError) => result.Switch(onSuccess, onError);

    /// <summary>
    /// Asynchronously handles both success and error cases by applying the appropriate function.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="resultTask">A task that represents the asynchronous operation to get the result to match on.</param>
    /// <param name="onSuccess">The asynchronous function to call if the result is successful.</param>
    /// <param name="onError">The asynchronous function to call if the result is an error.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the result of applying the appropriate function.</returns>
    public static async Task<TResult> MatchAsync<T, TResult>(
        this Task<Result<T>> resultTask,
        Func<Success<T>, Task<TResult>> onSuccess,
        Func<Error, Task<TResult>> onError)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SwitchAsync(onSuccess,onError).ConfigureAwait(false);
    }

    
    /// <summary>
    /// Performs an action with the success value if the result is successful.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="result">The result to perform the action on.</param>
    /// <param name="action">The action to perform with the success value.</param>
    /// <returns>The original result.</returns>
    public static Result<T> Tap<T>(this Result<T> result, Action<Success<T>> action)
        => result.Map(value =>
        {
            action(value);
            return value;
        });

    /// <summary>
    /// Asynchronously performs an action with the success value if the result is successful.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="resultTask">A task that represents the asynchronous operation to get the result to tap.</param>
    /// <param name="action">The asynchronous action to perform with the success value.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the original result.</returns>
    public static async Task<Result<T>> TapAsync<T>(
        this Task<Result<T>> resultTask,
        Func<Success<T>, Task> action)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SwitchAsync(
            async success =>
            { 
                await action(success).ConfigureAwait(false);
                return Result<T>.Success(success);
            },
            error => Task.FromResult<Result<T>>(error)
        ).ConfigureAwait(false);
    }




    /// <summary>
    /// Handles errors by applying the specified handler function if the result is an error.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="result">The result to handle errors for.</param>
    /// <param name="handler">The function that handles the error and returns a new result.</param>
    /// <returns>The original result if successful; otherwise, the result of the handler function.</returns>
    public static Result<T> HandleError<T>(
        this Result<T> result,
        Func<Error, Result<T>> handler)
        => result.Switch(_ => result, handler);

    
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="resultTask">A task that represents the asynchronous operation to get the result to handle errors for.</param>
    /// <param name="handler">The asynchronous function that handles the error and returns a new result.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the original result if successful;
    /// otherwise, the result of the handler function.
    /// </returns>
    /// <remarks>
    /// This method is useful for implementing error recovery or fallback logic in an asynchronous context.
    /// </remarks>
    public static async Task<Result<T>> HandleErrorAsync<T>(
        this Task<Result<T>> resultTask,
        Func<Error, Task<Result<T>>> handler)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SwitchAsync(_ => result.AsTask(), handler).ConfigureAwait(false);
    }

   
    /// <summary>
    /// Filters the success value using the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="result">The result to filter.</param>
    /// <param name="predicate">The predicate to apply to the success value.</param>
    /// <param name="error">The error to return if the predicate returns false.</param>
    /// <returns>
    /// The original result if the result is an error or the predicate returns true;
    /// otherwise, a failure result with the specified error.
    /// </returns>
    public static Result<T> Filter<T>(
        this Result<T> result,
        Func<Success<T>, bool> predicate,
        Error error) => result.Switch(
            value => predicate(value) ? result : Result<T>.Failure(error),
            _ => result
        );

    /// <summary>
    /// Asynchronously filters the success value using the specified predicate.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="resultTask">A task that represents the asynchronous operation to get the result to filter.</param>
    /// <param name="predicate">The asynchronous predicate to apply to the success value.</param>
    /// <param name="error">The error to return if the predicate returns false.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the filtered result.</returns>
    public static async Task<Result<T>> FilterAsync<T>(
        this Task<Result<T>> resultTask,
        Func<Success<T>, Task<bool>> predicate,
        Error error)
    {
        var result = await resultTask.ConfigureAwait(false);
        return await result.SwitchAsync(
            async success =>
            {
                var isMatch = await predicate(success).ConfigureAwait(false);
                return isMatch ? success : error;
            },
            _ => Task.FromResult(result)
        ).ConfigureAwait(false);
    }


    /// <summary>
    /// Converts a <see cref="Task{Result{T}}"/> to a <see cref="ValueTask{Result{T}}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="task">The task to convert.</param>
    /// <returns>A <see cref="ValueTask{Result{T}}"/> that represents the asynchronous operation.</returns>
    public static ValueTask<Result<T>> AsValueTask<T>(this Task<Result<T>> task)
        => new ValueTask<Result<T>>(task);
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to a <see cref="ValueTask{Result{T}}"/>.
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>A completed value-task containing the original result.</returns>
    ///  <remarks>
    /// This method is useful for converting a synchronous result to a value-task-based API.
    /// </remarks>
    public static ValueTask<Result<T>> AsValueTask<T>(this Result<T> result)=>ValueTask.FromResult(result);
    /// <summary>
    /// Converts a <see cref="Result{T}"/> to a completed <see cref="Task{Result{T}}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the result's success value.</typeparam>
    /// <param name="result">The result to convert to a task.</param>
    /// <returns>A completed task containing the original result.</returns>
    /// <remarks>
    /// This method is useful for converting a synchronous result to a task-based API.
    /// </remarks>
    public static Task<Result<T>> AsTask<T>(this Result<T> result) => Task.FromResult(result);

}