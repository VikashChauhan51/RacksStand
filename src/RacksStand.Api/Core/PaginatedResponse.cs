namespace RacksStand.Api.Core;

public record PaginatedResponse<T>(
    IEnumerable<T> Items,
    int CurrentPage,
    int PageSize,
    long TotalCount)
{
    public long TotalPages => (long)Math.Ceiling((double)TotalCount / PageSize);
}

