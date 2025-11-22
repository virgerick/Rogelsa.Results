namespace Rogelsa.Results.Failures;

/// <summary>
/// Represents an error that occurs when a requested resource could not be found.
/// </summary>
/// <param name="Resource">The resource that was not found, if known. This can provide additional context about the missing resource.</param>
/// <remarks>
/// This error typically indicates that the requested resource does not exist or the current user does not have permission to access it.
/// </remarks>
public record NotFound(Resource? Resource = null) : Error("Resource not found");