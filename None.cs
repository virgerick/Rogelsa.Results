namespace Rogelsa.Results;

/// <summary>
///     Represent a no value markup
/// </summary>
public readonly record struct None
{
    public static None Value => new();
}