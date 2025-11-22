namespace Rogelsa.Results.Paggination;

public sealed record Paged<T>(IEnumerable<T> Items, PagedInfo Info);

public sealed record PagedInfo(int Page, int PageSize, int TotalItems)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    public int Skip => (Page - 1) * PageSize;
    public bool HasPrevious => Page > 1;
    public bool HasNext => Page < TotalItems;
}

public readonly record struct PagedBuilder<T>
{
    public PagedBuilder(IEnumerable<T>? source = null)
    {
        Source = source ?? [];
    }

    private IEnumerable<T> Source { get; init; } = [];
    private int Page { get; init; }
    private int PageSize { get; init; }
    private int? TotalItems { get; init; }

    public static PagedBuilder<T> Create()
    {
        return new PagedBuilder<T>();
    }

    public PagedBuilder<T> WithPage(int page, int pageSize)
    {
        return this with { Page = page, PageSize = pageSize };
    }

    public PagedBuilder<T> WithSource(IEnumerable<T> source)
    {
        return this with { Source = source };
    }

    public PagedBuilder<T> WithTotal(int total)
    {
        return this with { TotalItems = total };
    }

    public Paged<T> Build()
    {
        var total = TotalItems ?? Source.Count();
        var info = new PagedInfo(Page, PageSize, total);
        var items = Source.Skip(info.Skip).Take(info.PageSize);
        return new Paged<T>(items, info);
    }
}