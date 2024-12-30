using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Features.Users.Domain.Entities;

namespace RacksStand.Api.Core.Domain.Entities;
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
