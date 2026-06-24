namespace GoPractice.Shared.Results;

public class PagedResult<T>
{
    public long TotalCount { get; init; }

    public IReadOnlyCollection<T> Items { get; init; } = Array.Empty<T>();
}
