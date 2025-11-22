namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents an error that occurs when the server understands the request but refuses to authorize it.
/// </summary>
/// <param name="Message">A message that explains why the request was forbidden.</param>
/// <remarks>
/// This error occurs when the server understood the request but will not fulfill it due to insufficient permissions.
/// The client should not repeat the request without changing the authentication or authorization.
/// </remarks>
public record Forbidden(string Message) : Error(Message);