using Rogelsa.Results.Succeed;

namespace Rogelsa.Results;

public static class ResultSuccessExtensions
{
    extension(Result result)
    {
        public static Result Ok()
        {
            return Result.Success(Success.Ok(None.Value));
        }

        public static Result<T> Ok<T>(T value)
        {
            return Result<T>.Success(Success.Ok(value));
        }

        public static Result NoContent()
        {
            return Result.Success(new NoContent<None>());
        }

        public static Result<T> NoContent<T>(T value)
        {
            return Result<T>.Success(Success.Ok(value));
        }

        public static Result Created(Resource? resource = null)
        {
            return Result.Success(new Created<None>(resource));
        }

        public static Result<T> Created<T>(T value)
        {
            return Result<T>.Success(Success.Ok(value));
        }

        public static Result Deleted(Resource? resource = null)
        {
            return Result.Success(new Deleted<None>(resource));
        }

        public static Result<T> Deleted<T>(T value)
        {
            return Result<T>.Success(Success.Ok(value));
        }
    }
}