namespace Rogelsa.Results.Failures;

public record Failure(string Message) : Error(Message);