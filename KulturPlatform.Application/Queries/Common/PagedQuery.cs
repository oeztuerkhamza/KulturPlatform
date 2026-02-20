using MediatR;

namespace KulturPlatform.Application.Queries.Common;

/// <summary>
/// Base class for paginated queries
/// </summary>
public abstract class PagedQuery
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 20;
    
    private int _pageNumber = 1;
    private int _pageSize = DefaultPageSize;

    /// <summary>
    /// Page number (1-based). Default is 1.
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    /// <summary>
    /// Number of items per page. Default is 20, maximum is 100.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value switch
        {
            < 1 => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            _ => value
        };
    }

    /// <summary>
    /// Calculates the number of items to skip
    /// </summary>
    public int Skip => (PageNumber - 1) * PageSize;
}
