using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class UserPlan : Entity<UserPlanId>
{
    public required string UserId { get; set; }
    public required PlanId PlanId { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = default!;
    public virtual Plan Plan { get; set; } = default!;
}
