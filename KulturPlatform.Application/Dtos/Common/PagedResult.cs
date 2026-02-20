namespace KulturPlatform.Application.Dtos.Common;

/// <summary>
/// Represents a paginated result set
/// </summary>
/// <typeparam name="T">Type of items in the page</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    /// <summary>
    /// Indicates if there is a previous page
    /// </summary>
    public bool HasPrevious => PageNumber > 1;

    /// <summary>
    /// Indicates if there is a next page
    /// </summary>
    public bool HasNext => PageNumber < TotalPages;

    /// <summary>
    /// Items in the current page
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Creates a new empty paged result
    /// </summary>
    public PagedResult()
    {
    }

    /// <summary>
    /// Creates a new paged result
    /// </summary>
    public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
    {
        Items = items;
        TotalCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    /// <summary>
    /// Creates an empty paged result
    /// </summary>
    public static PagedResult<T> Empty(int pageNumber = 1, int pageSize = 20)
    {
        return new PagedResult<T>(new List<T>(), 0, pageNumber, pageSize);
    }
}
