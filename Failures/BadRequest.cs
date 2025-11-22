namespace Rogelsa.Results.Failures;

public record BadRequest(params ValidationFailure[] Failures) : Error("One or more validation error occured");