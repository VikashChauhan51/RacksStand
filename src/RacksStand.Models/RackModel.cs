namespace RacksStand.Models;
public record RackModel(
    string Id,
    string Name,
    string Code,
    string? Label,
    string? Description,
    string StoreId
);
