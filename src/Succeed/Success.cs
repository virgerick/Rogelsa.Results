namespace Rogelsa.Results.Succeed;

/// <summary>
/// Represents a base class for successful operation results with a value.
/// </summary>
/// <typeparam name="TValue">The type of the value associated with the success result.</typeparam>
public abstract record Success<TValue>;
/// <summary>
/// Provides factory methods for creating various types of success results.
/// </summary>
public static class Success
{
    
    /// <summary>
    /// Creates a new <see cref="NoContent{TValue}"/> instance with no value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that could have been returned.</typeparam>
    /// <returns>A new <see cref="NoContent{TValue}"/> instance.</returns>
    public static NoContent<TValue> NoContent<TValue>()
    {
        return new NoContent<TValue>();
    }

    /// <summary>
    /// Creates a new non-generic <see cref="NoContent"/> instance.
    /// </summary>
    /// <returns>A new <see cref="NoContent"/> instance.</returns>
    public static NoContent NoContent()
    {
        return new NoContent();
    }

    /// <summary>
    /// Creates a new <see cref="Ok{TValue}"/> instance with the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="value">The value to include in the success result.</param>
    /// <returns>A new <see cref="Ok{TValue}"/> instance containing the specified value.</returns>
    public static Ok<TValue> Ok<TValue>(TValue value)
    {
        return new Ok<TValue>(value);
    }

    /// <summary>
    /// Creates a new non-generic <see cref="Ok"/> instance.
    /// </summary>
    /// <returns>A new <see cref="Ok"/> instance.</returns>
    public static Ok Ok()
    {
        return new Ok();
    }

    /// <summary>
    /// Creates a new <see cref="Created"/> instance indicating a resource was successfully created.
    /// </summary>
    /// <param name="resource">Optional resource that was created. Can be used to generate a location header.</param>
    /// <returns>A new <see cref="Created"/> instance.</returns>
    public static Created Created(Resource? resource = null)
    {
        return new Created(resource);
    }

    /// <summary>
    /// Creates a new <see cref="Created{TValue}"/> instance indicating a resource was successfully created.
    /// </summary>
    /// <typeparam name="TValue">The type of the created resource.</typeparam>
    /// <param name="value"></param>
    /// <param name="resource">Optional resource that was created. Can be used to generate a location header.</param>
    /// <returns>A new <see cref="Created{TValue}"/> instance.</returns>
    public static Created<TValue> Created<TValue>(TValue value,Resource? resource = null)
    {
        return new Created<TValue>(value, resource);
    }

    /// <summary>
    /// Creates a new <see cref="Deleted"/> instance indicating a resource was successfully deleted.
    /// </summary>
    /// <param name="resource">Optional resource that was deleted. Can be used for logging or generating a response.</param>
    /// <returns>A new <see cref="Deleted"/> instance.</returns>
    public static Deleted Deleted(Resource? resource = null)
    {
        return new Deleted(resource);
    }

    /// <summary>
    /// Creates a new <see cref="Deleted{TValue}"/> instance indicating a resource was successfully deleted.
    /// </summary>
    /// <typeparam name="TValue">The type of the deleted resource.</typeparam>
    /// <param name="resource">Optional resource that was deleted. Can be used for logging or generating a response.</param>
    /// <returns>A new <see cref="Deleted{TValue}"/> instance.</returns>
    public static Deleted<TValue> Deleted<TValue>(Resource? resource = null)
    {
        return new Deleted<TValue>(resource);
    }
}