namespace Rogelsa.Results.Failures;

public readonly record struct ValidationsErrorsBuilder(params ValidationFailure[] Errors)
{
    public static ValidationsErrorsBuilder Create()
    {
        return new ValidationsErrorsBuilder();
    }

    public ValidationsErrorsBuilder Add(string propertyName, string message)
    {
        return new ValidationsErrorsBuilder([..Errors, new ValidationFailure(propertyName, message)]);
    }

    public ValidationsErrorsBuilder Add(ValidationFailure error)
    {
        return new ValidationsErrorsBuilder([..Errors, error]);
    }

    public ValidationsErrorsBuilder Add(IEnumerable<ValidationFailure> errors)
    {
        return new ValidationsErrorsBuilder([..Errors, ..errors]);
    }
}