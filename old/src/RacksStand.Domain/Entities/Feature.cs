using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Feature : Entity<FeatureId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = [];
}
