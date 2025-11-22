namespace Rogelsa.Results.Succeed;

public record Deleted<TValue>(Resource? Resource = null) : Success<TValue>;

public record Deleted(Resource? Resource = null) : Deleted<None>(Resource);