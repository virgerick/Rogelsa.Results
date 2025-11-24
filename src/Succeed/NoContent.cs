namespace Rogelsa.Results.Succeed;

/// <summary>
/// Represents a successful operation that intentionally returns no content.
/// This is typically used for operations that complete successfully but have no data to return.
/// </summary>
/// <typeparam name="TValue">The type of value that would be returned if there was content.</typeparam>
public record NoContent<TValue> : Success<TValue>;

/// <summary>
/// A non-generic version of <see cref="NoContent{TValue}"/> that doesn't carry any value.
/// This is equivalent to <see cref="NoContent{TValue}"/> where TValue is <see cref="None"/>.
/// </summary>
public record NoContent : NoContent<None>;