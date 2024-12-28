using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;

namespace RacksStand.Persistence.Repositories;
public interface IUserPlanRepository : IRepository<UserPlan, UserPlanId>
{
    Task<IEnumerable<UserPlan>> GetActiveUserPlansAsync(CancellationToken cancellationToken);
    Task<IEnumerable<UserPlan>> GetUserPlansByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<UserPlan?> GetUserPlanDetailsAsync(UserPlanId userPlanId, CancellationToken cancellationToken);
}
