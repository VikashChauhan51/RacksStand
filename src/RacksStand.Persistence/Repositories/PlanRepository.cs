using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;


namespace RacksStand.Persistence.Repositories;
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

    public async Task<Plan?> GetPlanDetailsAsync(PlanId planId, CancellationToken cancellationToken)
    {
        return await Context.Set<Plan>()
                            .Include(p => p.Users)
                            .Include(p => p.UserPlans)
                            .Include(p => p.PlanFeatures)
                            .ThenInclude(x => x.Feature)
                            .FirstOrDefaultAsync(p => p.Id == planId, cancellationToken);
    }
}
