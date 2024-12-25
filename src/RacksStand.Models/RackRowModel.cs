namespace RacksStand.Models;
public record RackRowModel(
    string Id,
    string RackId,
    string Code,
    int RowNumber,
    string Label,
    string? Description
);
