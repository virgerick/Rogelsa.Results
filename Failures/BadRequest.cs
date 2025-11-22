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
public record BadRequest(params ValidationFailure[] Failures) : Error("One or more validation errors occurred");