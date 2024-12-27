namespace RacksStand.Models;
public record PlanModel
(
     string Id,
     string Name,
     decimal Price,
     string? SupportLevel,
     string Description
);
