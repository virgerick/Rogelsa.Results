namespace Rogelsa.Results.Succeed;

public record Created<TValue>(Resource? Resource = null) : Success<TValue>;

public record Created(Resource? Resource = null) : Created<None>(Resource);