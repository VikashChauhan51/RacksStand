using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.Enums;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Repositories;
public class PaymentRepository : Repository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<Payment>> GetPaymentsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await Context.Set<Payment>()
                            .Where(p => p.UserId == userId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByPlanIdAsync(PlanId planId, CancellationToken cancellationToken)
    {
        return await Context.Set<Payment>()
                            .Where(p => p.PlanId == planId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByStatusAsync(PaymentState status, CancellationToken cancellationToken)
    {
        return await Context.Set<Payment>()
                            .Where(p => p.Status == status)
                            .ToListAsync(cancellationToken);
    }

    public async Task<Payment?> GetPaymentDetailsAsync(PaymentId paymentId, CancellationToken cancellationToken)
    {
        return await Context.Set<Payment>()
                            .Include(p => p.User)
                            .Include(p => p.Plan)
                            .FirstOrDefaultAsync(p => p.Id == paymentId, cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetFailedPaymentsAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Payment>()
                            .Where(p => p.Status == PaymentState.Failed)
                            .ToListAsync(cancellationToken);
    }
}

