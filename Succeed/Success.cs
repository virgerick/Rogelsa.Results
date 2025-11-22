namespace Rogelsa.Results.Succeed;

public abstract record Success<TValue>;
public static class Success
{
    public static NoContent<TValue> NoContent<TValue>()
    {
        return new NoContent<TValue>();
    }

    public static NoContent NoContent()
    {
        return new NoContent();
    }

    public static Ok<TValue> Ok<TValue>(TValue value)
    {
        return new Ok<TValue>(value);
    }

    public static Ok Ok()
    {
        return new Ok();
    }

    public static Created Created(Resource? resource = null)
    {
        return new Created(resource);
    }

    public static Created<TValue> Created<TValue>(Resource? resource = null)
    {
        return new Created<TValue>(resource);
    }

    public static Deleted Deleted(Resource? resource = null)
    {
        return new Deleted(resource);
    }

    public static Deleted<TValue> Deleted<TValue>(Resource? resource = null)
    {
        return new Deleted<TValue>(resource);
    }
}