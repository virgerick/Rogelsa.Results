namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents an error that occurs when authentication is required but has failed or has not been provided.
/// </summary>
/// <param name="Message">A message that explains the reason for the authentication failure.</param>
/// <remarks>
/// This error occurs when the request requires user authentication information, but the request
/// doesn't include valid authentication credentials or the provided credentials are invalid.
/// The client may repeat the request with appropriate authentication information.
/// </remarks>
public record Unauthorized(string Message) : Error(Message);