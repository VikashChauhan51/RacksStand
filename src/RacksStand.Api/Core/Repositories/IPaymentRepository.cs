using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.Enums;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Repositories;
public interface IPaymentRepository : IRepository<Payment, PaymentId>
{
    Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByPlanIdAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentState status, CancellationToken cancellationToken);
    Task<Payment?> GetPaymentDetailsAsync(PaymentId paymentId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetFailedPaymentsAsync(CancellationToken cancellationToken);
}

