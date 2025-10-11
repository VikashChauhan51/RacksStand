using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Plan : Entity<PlanId>
{
    public required string Name { get; set; }
    public required PlanPrice Price { get; set; }
    public int DurationInMonths { get; set; }
    public string? SupportLevel { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = [];
}
