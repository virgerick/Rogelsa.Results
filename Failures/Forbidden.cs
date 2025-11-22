namespace Rogelsa.Results.Failures;

public record Forbidden(string Message) : Error(Message);