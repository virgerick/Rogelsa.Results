namespace Rogelsa.Results.Succeed;

public record NoContent<TValue> : Success<TValue>;

public record NoContent : NoContent<None>;