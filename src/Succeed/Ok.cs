namespace Rogelsa.Results.Succeed;

/// <summary>
/// Represents a successful operation result with a value of type <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
/// <param name="Value">The value resulting from a successful operation.</param>
public record Ok<TValue>(TValue Value) : Success<TValue>;

/// <summary>
/// Represents a successful operation result with no specific value (equivalent to <see cref="Ok{TValue}"/> where TValue is <see cref="None"/>).
/// </summary>
public record Ok() : Ok<None>(None.Value);