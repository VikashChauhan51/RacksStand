using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface IPaymentRepository : IRepository<Payment, PaymentId>
{
    Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByPlanIdAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentState status, CancellationToken cancellationToken);
    Task<Payment?> GetPaymentDetailsAsync(PaymentId paymentId, CancellationToken cancellationToken);
    Task<IEnumerable<Payment>> GetFailedPaymentsAsync(CancellationToken cancellationToken);
}

