using RacksStand.Persistence.Entities;
using RacksStand.Persistence.Enums;
using RacksStand.Persistence.ValueObjects;


namespace RacksStand.Persistence.Repositories;
public interface IPaymentRepository : IRepository<Payment, PaymentId>
{
    Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByPlanIdAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentState status, CancellationToken cancellationToken);
    Task<Payment?> GetPaymentDetailsAsync(PaymentId paymentId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetFailedPaymentsAsync(CancellationToken cancellationToken);
}

