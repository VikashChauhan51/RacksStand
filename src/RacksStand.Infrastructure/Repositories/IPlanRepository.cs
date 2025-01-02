using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface IPlanRepository : IRepository<Plan, PlanId>
{
    Task<Plan?> GetPlanWithFeaturesAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Plan>> GetPlansWithFeaturesAsync(CancellationToken cancellationToken);
    Task<Plan?> GetPlanDetailsAsync(PlanId planId, CancellationToken cancellationToken);
}
