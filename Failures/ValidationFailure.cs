namespace Rogelsa.Results.Failures;

public readonly record struct ValidationFailure(string PropertyName, string Message);