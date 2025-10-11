using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class PlanFeature : Entity<PlanFeatureId>
{
    public required PlanId PlanId { get; set; } 
    public required FeatureId FeatureId { get; set; } 
    public required string Value { get; set; }

    // Navigation properties
    public virtual Plan Plan { get; set; } = null!; 
    public virtual Feature Feature { get; set; } = null!; 
}
