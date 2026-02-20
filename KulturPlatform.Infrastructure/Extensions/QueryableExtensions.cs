using KulturPlatform.Application.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace KulturPlatform.Infrastructure.Extensions;

/// <summary>
/// Extension methods for pagination
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Applies pagination to a queryable and returns a PagedResult
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="query">Source query</param>
    /// <param name="pageNumber">Page number (1-based)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>PagedResult with items and pagination metadata</returns>
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        // Ensure valid values
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 20 : pageSize > 100 ? 100 : pageSize;

        // Get total count
        var totalCount = await query.CountAsync(cancellationToken);

        // Get items for current page
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(items, totalCount, pageNumber, pageSize);
    }

    /// <summary>
    /// Applies pagination to a queryable (Skip/Take only)
    /// </summary>
    public static IQueryable<T> ApplyPaging<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        // Ensure valid values
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 20 : pageSize > 100 ? 100 : pageSize;

        return query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}
