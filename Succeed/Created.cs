namespace Rogelsa.Results.Succeed;

/// <summary>
/// Represents a successful operation that resulted in the creation of a resource.
/// </summary>
/// <typeparam name="TValue">The type of the created resource.</typeparam>
/// <param name="Value">The value of the created resource.</param>
/// <param name="Resource">Optional metadata about the created resource, typically used for generating location headers.</param>
public record Created<TValue>(TValue Value, Resource? Resource = null) : Success<TValue>;

/// <summary>
/// A non-generic version of <see cref="Created{TValue}"/> for operations that don't return a resource.
/// This is equivalent to <see cref="Created{TValue}"/> where TValue is <see cref="None"/>.
/// </summary>
/// <param name="Resource">Optional metadata about the created resource, typically used for generating location headers.</param>
public record Created(Resource? Resource = null) : Created<None>(None.Value, Resource);