namespace RacksStand.Models;
public record PocketModel(
    string Id,
    string RackRowId,
    string Code,
    int ColumnNumber,
    int? Capacity,
    string State
);
