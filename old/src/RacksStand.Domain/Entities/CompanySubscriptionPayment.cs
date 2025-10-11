using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class CompanySubscriptionPayment : Entity<CompanySubscriptionPaymentId>
{
    public required CompanySubscriptionId CompanySubscriptionId { get; set; }
    public required PaymentAmount Amount { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }
    public TransactionId? TransactionId { get; set; }
    public required DateTimeOffset PaymentDate { get; set; }
    public required PaymentState Status { get; set; }
    public string? FailureReason { get; set; }

    // Navigation properties
    public virtual CompanySubscription CompanySubscription { get; set; } = default!;
}
