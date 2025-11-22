namespace Rogelsa.Results.Failures;

public record NotFound(Resource? Resource = null) : Error("Resource not found");