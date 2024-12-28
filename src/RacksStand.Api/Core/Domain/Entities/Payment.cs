using RacksStand.Api.Core.Domain.Enums;
using RacksStand.Api.Core.Domain.ValueObjects;


namespace RacksStand.Api.Core.Domain.Entities;
public class Payment : Entity<PaymentId>
{
    public required string UserId { get; set; }
    public required PlanId PlanId { get; set; }
    public required PaymentAmount Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public TransactionId? TransactionId { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentState Status { get; set; }
    public string? FailureReason { get; set; }

    // Relationships
    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}
