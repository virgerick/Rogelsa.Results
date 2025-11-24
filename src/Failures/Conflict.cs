namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents an error that occurs when a request conflicts with the current state of the resource.
/// </summary>
/// <param name="Message">A message that describes the conflict, including which resource is involved and the nature of the conflict.</param>
/// <remarks>
/// This error typically occurs when a request cannot be completed due to a conflict with the current state of the resource,
/// such as when trying to create a resource that already exists or when an operation would violate a unique constraint.
/// </remarks>
public record Conflict(string Message) : Error(Message);