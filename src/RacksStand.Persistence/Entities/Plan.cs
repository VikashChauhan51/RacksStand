using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class Plan : Entity<PlanId>
{
    public required string Name { get; set; }
    public required PlanPrice Price { get; set; }
    public string? SupportLevel { get; set; }
    public required string Description { get; set; }

    // Navigation properties
    public virtual ICollection<PlanFeature> PlanFeatures { get; set; } = [];
    public virtual ICollection<User> Users { get; set; } = [];
    public virtual ICollection<UserPlan> UserPlans { get; set; } = [];
}
