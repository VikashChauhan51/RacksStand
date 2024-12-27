
namespace RacksStand.UnifiedServices.DataServices;

public interface IPlanService
{
    Task<Result<PlanDetailsModel>> GetPlanAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PlanDetailsModel>> GetPlansAsync(CancellationToken cancellationToken = default);
}