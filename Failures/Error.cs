namespace Rogelsa.Results.Failures;

public abstract record Error(string Message);

public static class ErrorExtensions
{
    extension(Error error)
    {
        public static Error Failure(string message)
        {
            return new Failure(message);
        }

        public static Error NotFound(string resourceName, string resourceId)
        {
            return new NotFound(new Resource(resourceName, resourceId));
        }

        public static Error NotFound(Resource? resource = null)
        {
            return new NotFound(resource);
        }

        public static Error BadRequest(params ValidationFailure[] validationFailures)
        {
            return new BadRequest(validationFailures);
        }

        public static Error Conflict(string message)
        {
            return new Conflict(message);
        }

        public static Error Unauthorized(string message)
        {
            return new Unauthorized(message);
        }

        public static Error Forbidden(string message)
        {
            return new Forbidden(message);
        }
    }
}