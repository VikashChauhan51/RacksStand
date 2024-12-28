using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;


namespace RacksStand.Persistence.Repositories;
public interface IPlanRepository : IRepository<Plan, PlanId>
{
    Task<Plan?> GetPlanWithFeaturesAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Plan>> GetPlansWithFeaturesAsync(CancellationToken cancellationToken);
    Task<Plan?> GetPlanDetailsAsync(PlanId planId, CancellationToken cancellationToken);
}
