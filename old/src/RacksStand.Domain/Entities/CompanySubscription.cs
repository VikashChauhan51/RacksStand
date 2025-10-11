using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class CompanySubscription : Entity<CompanySubscriptionId>
{
    public required CompanyId CompanyId { get; set; }
    public required PlanId PlanId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public bool IsTrial { get; set; } = true;
    public DateTimeOffset? RenewedAt { get; set; }

    // Navigation properties
    public virtual Plan Plan { get; set; } = default!;
    public virtual Company Company { get; set; } = default!;
}

