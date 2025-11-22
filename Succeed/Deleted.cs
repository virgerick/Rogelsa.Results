namespace Rogelsa.Results.Succeed;

/// <summary>
/// Represents a successful operation that resulted in the deletion of a resource.
/// </summary>
/// <typeparam name="TValue">The type of the resource that was deleted. This can be used to include additional information about the deleted resource.</typeparam>
/// <param name="Resource">Optional metadata about the deleted resource, which can be used for logging or generating audit information.</param>
public record Deleted<TValue>(Resource? Resource = null) : Success<TValue>;

/// <summary>
/// A non-generic version of <see cref="Deleted{TValue}"/> for operations that don't return any additional information.
/// This is equivalent to <see cref="Deleted{TValue}"/> where TValue is <see cref="None"/>.
/// </summary>
/// <param name="Resource">Optional metadata about the deleted resource, which can be used for logging or generating audit information.</param>
public record Deleted(Resource? Resource = null) : Deleted<None>(Resource);