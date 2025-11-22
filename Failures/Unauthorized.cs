namespace Rogelsa.Results.Failures;

public record Unauthorized(string Message) : Error(Message);