namespace Rogelsa.Results.Failures;

public abstract record Error(string Message);

/// <summary>
/// Provides extension methods for creating and working with <see cref="Error"/> instances.
/// </summary>
public static class ErrorExtensions
{
    extension(Error error)
    {

        /// <summary>
        /// Creates a new instance of the <see cref="Failures.Failure"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the failure.</param>
        /// <returns>A new <see cref="Failures.Failure"/> instance.</returns>
        public static Error Failure(string message)
        {
            return new Failure(message);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="NotFound"/> error for a specific resource.
        /// </summary>
        /// <param name="resourceName">The name of the resource type that was not found.</param>
        /// <param name="resourceId">The identifier of the resource that was not found.</param>
        /// <returns>A new <see cref="NotFound"/> error instance.</returns>
        public static Error NotFound(string resourceName, string resourceId)
        {
            return new NotFound(new Resource(resourceName, resourceId));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="NotFound"/> error.
        /// </summary>
        /// <param name="resource">The resource that was not found, if any.</param>
        /// <returns>A new <see cref="NotFound"/> error instance.</returns>
        public static Error NotFound(Resource? resource = null)
        {
            return new NotFound(resource);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BadRequest"/> error with validation failures.
        /// </summary>
        /// <param name="validationFailures">The validation failures that caused the bad request.</param>
        /// <returns>A new <see cref="BadRequest"/> error instance containing the validation failures.</returns>
        public static Error BadRequest(params ValidationFailure[] validationFailures)
        {
            return new BadRequest(validationFailures);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Conflict"/> error.
        /// </summary>
        /// <param name="message">The message that describes the conflict.</param>
        /// <returns>A new <see cref="Conflict"/> error instance.</returns>
        public static Error Conflict(string message)
        {
            return new Conflict(message);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Unauthorized"/> error.
        /// </summary>
        /// <param name="message">The message that explains the authorization failure.</param>
        /// <returns>A new <see cref="Unauthorized"/> error instance.</returns>
        public static Error Unauthorized(string message)
        {
            return new Unauthorized(message);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Forbidden"/> error.
        /// </summary>
        /// <param name="message">The message that explains why access was forbidden.</param>
        /// <returns>A new <see cref="Forbidden"/> error instance.</returns>
        public static Error Forbidden(string message)
        {
            return new Forbidden(message);
        }
    }
}
