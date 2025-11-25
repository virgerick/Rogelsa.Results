using Rogelsa.Results.Failures;

namespace Rogelsa.Results.UnitTests;

public class ErrorsTest
{
    [Fact]
    public void ErrorTypeBadRequestShouldHaveValidationError()
    {
        var errors = CreateFailures();
        
        Error error = Error.BadRequest(errors);
        
        Assert.NotEmpty(error.Message);
        var badRequest = Assert.IsType<BadRequest>(error);
        Assert.Equivalent(errors, badRequest.Failures);

    }

    [Fact]
    public void ErrorTypeBadRequestShouldNotHaveValidationError()
    {
        var failure = new ValidationFailure("Email", "Invalid format");
        
        var error = Error.BadRequest([failure]);
        Assert.NotEmpty(error.Message);
        var badRequest = Assert.IsType<BadRequest>(error);
        Assert.NotEmpty(badRequest.Failures);
        Assert.Equal(failure, badRequest.Failures.First());
        
    }
    private static ValidationFailure[] CreateFailures()
    {
        return BadRequest.Empty()
            .AddError(new ValidationFailure("Email", "Invalid format"))
            .AddError("Password", "Invalid format")
            .AddError("Username", "Invalid format")
            .Failures;
    }
    
    [Fact]
    public void ErrorTypeConflictShouldHaveAMessage()
    {
        const string errorMessage = "Conflict error message";
        Error error = Error.Conflict(errorMessage);
        Assert.NotEmpty(error.Message);
        var conflict = Assert.IsType<Conflict>(error);
        Assert.Equal(errorMessage, conflict.Message);

    }

    [Fact]
    public void ErrorTypeFailureShouldHaveAMessage()
    {
        const string errorMessage = "Failure error message";
        var error = Error.Failure(errorMessage);
        Assert.NotEmpty(error.Message);
        var failure = Assert.IsType<Failure>(error);
        Assert.Equal(errorMessage, failure.Message);
        
    }
}