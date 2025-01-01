using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class CompanyPlanPayment : Entity<CompanyPlanPaymentId>
{
    public required UserId UserId { get; set; }
    public required PlanId PlanId { get; set; }
    public required CompanyId CompanyId { get; set; }
    public required PaymentAmount Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public TransactionId? TransactionId { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentState Status { get; set; }
    public string? FailureReason { get; set; }

    // Relationships
    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
    public Company Company { get; set; } = null!;
}
