namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents an error that occurs when a request contains invalid data or fails validation.
/// </summary>
/// <param name="Failures">An array of <see cref="ValidationFailure"/> objects that describe the validation errors that occurred.</param>
/// <remarks>
/// This error is typically returned when the server cannot process the request due to client-side errors,
/// such as missing required fields, invalid data formats, or business rule violations.
/// The <see cref="Failures"/> collection provides detailed information about each validation error.
/// </remarks>
public record BadRequest(ValidationFailure[] Failures) : Error("One or more validation errors occurred");
public static class BadRequestExtensions
{
    extension(BadRequest badRequest)
    {
        public static BadRequest Empty() => new BadRequest([]);

        public BadRequest AddErrors(IEnumerable<ValidationFailure> failures) => badRequest with
        {
            Failures = [..badRequest.Failures, ..failures]
        };

        public BadRequest AddError(string property, string message) => badRequest with
        {
            Failures = [..badRequest.Failures, new ValidationFailure(property, message)]
        };

        public BadRequest AddError(ValidationFailure failure) => badRequest with
        {
            Failures = [..badRequest.Failures, failure]
        };
    }

}