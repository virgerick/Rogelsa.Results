namespace Rogelsa.Results.Succeed;

public record Ok<TValue>(TValue Value) : Success<TValue>;

public record Ok() : Ok<None>(None.Value);