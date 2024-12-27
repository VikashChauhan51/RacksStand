namespace RacksStand.Models;
public record PlanDetailsModel
(
     string Id,
     string Name,
     decimal Price,
     string? SupportLevel,
     string Description,
     IEnumerable<PlanFeatureDetailsModel> PlanFeatures
);
