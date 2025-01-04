using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface IPlanRepository : IRepository<Plan, PlanId>
{
    Task<Plan?> GetPlanWithFeaturesAsync(PlanId planId, CancellationToken cancellationToken);
    Task<IEnumerable<Plan>> GetPlansWithFeaturesAsync(CancellationToken cancellationToken);
}
