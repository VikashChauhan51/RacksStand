using Microsoft.EntityFrameworkCore;

namespace RacksStand.Api.Domain.Entities.Core;

public record PaginatedResult<T>
(
     IList<T> Items,
     long TotalCount,
     int PageNumber,
     int PageSize
)
{
    private const int DefaultPageSize = 10;
    private const int DefaultPageNumber = 1;
    private const int MaxPageSize = 500;
    private const int MaxPageNumber = 1000;
    private const int PageIndexOffset = 1;
    private const int DefaultZeroValue = 0;
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / (PageSize > DefaultZeroValue ? PageSize : DefaultPageNumber));
    public bool HasPreviousPage => PageNumber > DefaultPageNumber;
    public bool HasNextPage => PageNumber < TotalPages;
    public bool IsEmpty => (Items?.Count ?? DefaultZeroValue) == DefaultZeroValue;
    public static PaginatedResult<T> Empty() => new([], DefaultZeroValue, DefaultZeroValue, DefaultZeroValue);
    public static PaginatedResult<T> Create(IList<T> items, long totalCount, int pageNumber, int pageSize) =>
        new(items, totalCount, pageNumber, pageSize);
    public static PaginatedResult<T> FromList(IList<T> list, int pageNumber, int pageSize) =>
        new(list, list.Count, pageNumber, pageSize);
    public static PaginatedResult<T> FromQueryable(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        pageNumber = EnsureValue(pageNumber, DefaultPageNumber, MaxPageNumber);
        pageSize = EnsureValue(pageSize, DefaultPageSize, MaxPageSize);
        var totalCount = query.Count();
        var items = query.Skip((pageNumber - PageIndexOffset) * pageSize).Take(pageSize).ToList();
        return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
    }
    public static async Task<PaginatedResult<T>> FromQueryableAsync(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {

        pageNumber = EnsureValue(pageNumber, DefaultPageNumber, MaxPageNumber);
        pageSize = EnsureValue(pageSize, DefaultPageSize, MaxPageSize);
        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((pageNumber - PageIndexOffset) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
    }

    private static int EnsureValue(int currentValue, int defaultValue, int? maxValue = null)
    {
        if (currentValue <= DefaultZeroValue)
        {
            return defaultValue;
        }

        return maxValue.HasValue
            ? Math.Min(currentValue, maxValue.Value)
            : currentValue;
    }
}
