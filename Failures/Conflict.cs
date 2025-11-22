namespace Rogelsa.Results.Failures;

public record Conflict(string Message) : Error(Message);