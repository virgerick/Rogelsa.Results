using Rogelsa.Results.Failures;

namespace Rogelsa.Results.Extensions;

/// <summary>
/// Provides extension methods for creating failed <see cref="Result"/> and <see cref="Result{TValue}"/> instances.
/// </summary>
public static class ResultFailureExtensions
{
    /// <summary>
    /// Extension methods for <see cref="Result"/> and <see cref="Result{T}"/> to create failure results.
    /// </summary>
    extension(Result result)
    {
        /// <summary>
        /// Creates a new <see cref="Result"/> representing an unauthorized access failure.
        /// </summary>
        /// <param name="message">The error message describing the unauthorized access.</param>
        /// <returns>A failed <see cref="Result"/> with an unauthorized error.</returns>
        public static Result Unauthorized(string message)
        {
            return Result.Failure(Error.Unauthorized(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing an unauthorized access failure.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="message">The error message describing the unauthorized access.</param>
        /// <returns>A failed <see cref="Result{T}"/> with an unauthorized error.</returns>
        public static Result<T> Unauthorized<T>(string message)
        {
            return Result<T>.Failure(Error.Unauthorized(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> representing a conflict failure.
        /// </summary>
        /// <param name="message">The error message describing the conflict.</param>
        /// <returns>A failed <see cref="Result"/> with a conflict error.</returns>
        public static Result Conflict(string message)
        {
            return Result.Failure(Error.Conflict(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing a conflict failure.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="message">The error message describing the conflict.</param>
        /// <returns>A failed <see cref="Result{T}"/> with a conflict error.</returns>
        public static Result<T> Conflict<T>(string message)
        {
            return Result<T>.Failure(Error.Conflict(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> representing a forbidden access failure.
        /// </summary>
        /// <param name="message">The error message describing the forbidden access.</param>
        /// <returns>A failed <see cref="Result"/> with a forbidden error.</returns>
        public static Result Forbidden(string message)
        {
            return Result.Failure(Error.Forbidden(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing a forbidden access failure.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="message">The error message describing the forbidden access.</param>
        /// <returns>A failed <see cref="Result{T}"/> with a forbidden error.</returns>
        public static Result<T> Forbidden<T>(string message)
        {
            return Result<T>.Failure(Error.Forbidden(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> representing a general failure.
        /// </summary>
        /// <param name="message">The error message describing the failure.</param>
        /// <returns>A failed <see cref="Result"/> with the specified error message.</returns>
        public static Result Failure(string message)
        {
            return Result.Failure(Error.Failure(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing a general failure.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="message">The error message describing the failure.</param>
        /// <returns>A failed <see cref="Result{T}"/> with the specified error message.</returns>
        public static Result<T> Failure<T>(string message)
        {
            return Result<T>.Failure(Error.Failure(message));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> representing a not found failure.
        /// </summary>
        /// <param name="resource">Optional resource that was not found. If provided, it will be included in the error message.</param>
        /// <returns>A failed <see cref="Result"/> with a not found error.</returns>
        public static Result NotFound(Resource? resource = null)
        {
            return Result.Failure(Error.NotFound(resource));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing a not found failure.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="resource">Optional resource that was not found. If provided, it will be included in the error message.</param>
        /// <returns>A failed <see cref="Result{T}"/> with a not found error.</returns>
        public static Result<T> NotFound<T>(Resource? resource = null)
        {
            return Result<T>.Failure(Error.NotFound(resource));
        }

        /// <summary>
        /// Creates a new <see cref="Result"/> representing a bad request failure with validation errors.
        /// </summary>
        /// <param name="validationFailures">The validation failures that caused the bad request.</param>
        /// <returns>A failed <see cref="Result"/> with validation errors.</returns>
        public static Result BadRequest(params ValidationFailure[] validationFailures)
        {
            return Result.Failure(Error.BadRequest(validationFailures));
        }

        /// <summary>
        /// Creates a new <see cref="Result{T}"/> representing a bad request failure with validation errors.
        /// </summary>
        /// <typeparam name="T">The type of the expected value in case of success.</typeparam>
        /// <param name="validationFailures">The validation failures that caused the bad request.</param>
        /// <returns>A failed <see cref="Result{T}"/> with validation errors.</returns>
        public static Result<T> BadRequest<T>(params ValidationFailure[] validationFailures)
        {
            return Result<T>.Failure(Error.BadRequest(validationFailures));
        }
    }
}