using Rogelsa.Results.Failures;

namespace Rogelsa.Results;

public static class ResultFailureExtensions
{
    extension(Result result)
    {
        public static Result Unauthorized(string message)
        {
            return Result.Failure(Error.Unauthorized(message));
        }

        public static Result<T> Unauthorized<T>(string message)
        {
            return Result<T>.Failure(Error.Unauthorized(message));
        }

        public static Result Conflict(string message)
        {
            return Result.Failure(Error.Conflict(message));
        }

        public static Result<T> Conflict<T>(string message)
        {
            return Result<T>.Failure(Error.Conflict(message));
        }

        public static Result Forbidden(string message)
        {
            return Result.Failure(Error.Forbidden(message));
        }

        public static Result<T> Forbidden<T>(string message)
        {
            return Result<T>.Failure(Error.Forbidden(message));
        }

        public static Result Failure(string message)
        {
            return Result.Failure(Error.Failure(message));
        }

        public static Result<T> Failure<T>(string message)
        {
            return Result<T>.Failure(Error.Failure(message));
        }

        public static Result NotFound(Resource? resource = null)
        {
            return Result.Failure(Error.NotFound(resource));
        }

        public static Result<T> NotFound<T>(Resource? resource = null)
        {
            return Result<T>.Failure(Error.NotFound(resource));
        }

        public static Result BadRequest(params ValidationFailure[] validationFailures)
        {
            return Result.Failure(Error.BadRequest(validationFailures));
        }

        public static Result<T> BadRequest<T>(params ValidationFailure[] validationFailures)
        {
            return Result<T>.Failure(Error.BadRequest(validationFailures));
        }
    }
}