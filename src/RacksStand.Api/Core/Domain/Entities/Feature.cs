using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Entities;
public class Feature : Entity<FeatureId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = [];
}
