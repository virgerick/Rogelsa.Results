using Rogelsa.Results.Succeed;

namespace Rogelsa.Results.Extensions;

/// <summary>
/// Provides extension methods for creating successful <see cref="Result"/> and <see cref="Result{TValue}"/> instances.
/// </summary>
public static class ResultSuccessExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Result"/> and <see cref="Result{T}"/> to create success results.
    /// </summary>
    extension(Result result)
    {
        /// <summary>
        /// Creates a new successful <see cref="Result"/> with an OK status.
        /// </summary>
        /// <returns>A successful <see cref="Result"/> with an OK status.</returns>
        public static Result Ok()
        {
            return Result.Success(Success.Ok(None.Value));
        }

        /// <summary>
        /// Creates a new successful <see cref="Result{T}"/> with the specified value and OK status.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to include in the result.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the specified value with an OK status.</returns>
        public static Result<T> Ok<T>(T value)
        {
            return Result<T>.Success(Success.Ok(value));
        }

        /// <summary>
        /// Creates a new successful <see cref="Result"/> with a NoContent status.
        /// This is typically used for successful operations that don't return any content.
        /// </summary>
        /// <returns>A successful <see cref="Result"/> with a NoContent status.</returns>
        public static Result NoContent()
        {
            return Result.Success(Success.NoContent());
        }

/// <summary>
        /// Creates a new successful <see cref="Result{T}"/> with the specified value and OK status.
        /// Note: This method creates an OK result, not a NoContent result, due to the presence of a value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value to include in the result.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the specified value with an OK status.</returns>
        /// <remarks>
        /// This method creates an OK result rather than a NoContent result because a NoContent 
        /// response typically indicates that there is no content to return, which conflicts with 
        /// providing a value. Consider using the non-generic <see cref="NoContent()"/> if you 
        /// don't need to return a value.
        /// </remarks>
        public static Result<T> NoContent<T>()
        {
            return Result<T>.Success(Success.NoContent<T>());
        }

        /// <summary>
        /// Creates a new successful <see cref="Result"/> with a Created status.
        /// This is typically used for successful resource creation operations.
        /// </summary>
        /// <param name="resource">Optional resource that was created. If provided, it may be used to generate a location header.</param>
        /// <returns>A successful <see cref="Result"/> with a Created status.</returns>
        public static Result Created(Resource? resource = null)
        {
            return Result.Success(Success.Created(resource));
        }

        /// <summary>
        /// Creates a new successful <see cref="Result{T}"/> with the specified value and Created status.
        /// This is typically used for successful resource creation operations.
        /// </summary>
        /// <typeparam name="T">The type of the created resource.</typeparam>
        /// <param name="value">The created resource value.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the created resource with a Created status.</returns>
        public static Result<T> Created<T>(T value)
        {
            return Result<T>.Success(Success.Created(value));
        }

        /// <summary>
        /// Creates a new successful <see cref="Result"/> indicating that a resource was successfully deleted.
        /// </summary>
        /// <param name="resource">Optional resource that was deleted. This can be used for logging or generating a response body.</param>
        /// <returns>A successful <see cref="Result"/> indicating a successful deletion.</returns>
        public static Result Deleted(Resource? resource = null)
        {
            return Result.Success(Success.Deleted(resource));
        }

        /// <summary>
        /// Creates a new successful <see cref="Result{T}"/> with the specified value indicating a successful deletion.
        /// </summary>
        /// <typeparam name="T">The type of the value to return.</typeparam>
        /// <param name="value">The value to include in the result, typically information about the deleted resource.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the specified value.</returns>
        /// <remarks>
        /// This method creates an OK result rather than a NoContent result because it returns a value.
        /// If you don't need to return a value, consider using the non-generic <see cref="Deleted(Resource?)"/> method.
        /// </remarks>
        public static Result<T> Deleted<T>(Resource? resource = null)
        {
            return Result<T>.Success(Success.Deleted<T>(resource));
        }
    }
}