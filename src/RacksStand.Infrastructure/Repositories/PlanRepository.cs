using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Api.Core.Infrastructure.Repositories;
public class PlanRepository : Repository<Plan, PlanId>, IPlanRepository
{
    public PlanRepository(DbContext context) : base(context) { }


    public async Task<Plan?> GetPlanWithFeaturesAsync(PlanId planId, CancellationToken cancellationToken)
    {
        return await Context.Set<Plan>()
                            .Include(p => p.PlanFeatures)
                            .ThenInclude(x => x.Feature)
                            .FirstOrDefaultAsync(p => p.Id == planId, cancellationToken);
    }
    public async Task<IEnumerable<Plan>> GetPlansWithFeaturesAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<Plan>()
                            .Include(p => p.PlanFeatures)
                            .ThenInclude(x => x.Feature)
                            .ToListAsync(cancellationToken);
    }
}
