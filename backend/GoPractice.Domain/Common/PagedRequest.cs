namespace GoPractice.Domain.Common;

public class PagedRequest
{
    private const int MaxPageSize = 100;

    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 20;

    public string? Keyword { get; set; }

    public int SafePageIndex => PageIndex < 1 ? 1 : PageIndex;

    public int SafePageSize => PageSize switch
    {
        < 1 => 20,
        > MaxPageSize => MaxPageSize,
        _ => PageSize
    };
}
