using RacksStand.Models;

namespace RacksStand.Api.Core.Infrastructure.Services;

public interface IPlanService
{
    Task<Result<PlanDetailsModel>> GetPlanAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlanDetailsModel>> GetPlansAsync(CancellationToken cancellationToken = default);
}
