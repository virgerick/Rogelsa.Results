namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents a validation error for a specific property in a request.
/// </summary>
/// <param name="PropertyName">The name of the property that failed validation.</param>
/// <param name="Message">The validation error message that describes the failure.</param>
/// <remarks>
/// This record is used to provide detailed information about validation failures that occur
/// when processing a request. Each instance represents a single validation error for a specific property.
/// </remarks>
public readonly record struct ValidationFailure(string PropertyName, string Message);