namespace Rogelsa.Results.Failures;

/// <summary>
/// A builder class for creating and managing a collection of <see cref="ValidationFailure"/> instances.
/// </summary>
/// <param name="Errors">An optional array of initial validation errors.</param>
/// <remarks>
/// This builder provides a fluent API for constructing a collection of validation errors
/// that can be used with the <see cref="BadRequest"/> error type.
/// </remarks>
public readonly record struct ValidationsErrorsBuilder(params ValidationFailure[] Errors)
{
    /// <summary>
    /// Creates a new, empty instance of the <see cref="ValidationsErrorsBuilder"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="ValidationsErrorsBuilder"/> with no validation errors.</returns>
    public static ValidationsErrorsBuilder Create()
    {
        return new ValidationsErrorsBuilder();
    }

    /// <summary>
    /// Adds a new validation error with the specified property name and error message.
    /// </summary>
    /// <param name="propertyName">The name of the property that failed validation.</param>
    /// <param name="message">The validation error message.</param>
    /// <returns>A new <see cref="ValidationsErrorsBuilder"/> with the added validation error.</returns>
    public ValidationsErrorsBuilder Add(string propertyName, string message)
    {
        return new ValidationsErrorsBuilder([..Errors, new ValidationFailure(propertyName, message)]);
    }

    /// <summary>
    /// Adds a <see cref="ValidationFailure"/> instance to the collection of validation errors.
    /// </summary>
    /// <param name="error">The validation error to add.</param>
    /// <returns>A new <see cref="ValidationsErrorsBuilder"/> with the added validation error.</returns>
    public ValidationsErrorsBuilder Add(ValidationFailure error)
    {
        return new ValidationsErrorsBuilder([..Errors, error]);
    }

    /// <summary>
    /// Adds multiple validation errors to the collection.
    /// </summary>
    /// <param name="errors">The validation errors to add.</param>
    /// <returns>A new <see cref="ValidationsErrorsBuilder"/> with the added validation errors.</returns>
    public ValidationsErrorsBuilder Add(IEnumerable<ValidationFailure> errors)
    {
        return new ValidationsErrorsBuilder([..Errors, ..errors]);
    }
}