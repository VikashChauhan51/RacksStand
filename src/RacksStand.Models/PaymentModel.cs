namespace RacksStand.Models;
public record PaymentModel
(
    string Id,
    string UserId,
    string PlanId,
    decimal Amount,
    string PaymentMethod,
    string? TransactionId,
    DateTime PaymentDate,
    string Status,
    string? FailureReason
);
