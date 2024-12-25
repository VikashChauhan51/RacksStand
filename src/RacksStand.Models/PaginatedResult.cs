namespace RacksStand.Models;
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; init; } = [];
    public int TotalItems { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
