using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public class UserPlanRepository : Repository<UserPlan, UserPlanId>, IUserPlanRepository
{
    public UserPlanRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<UserPlan>> GetActiveUserPlansAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<UserPlan>()
                            .Where(up => (up.EndDate == null || up.EndDate > DateTime.UtcNow) && up.IsActive)
                            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserPlan>> GetUserPlansByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await Context.Set<UserPlan>()
                            .Where(up => up.UserId == userId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<UserPlan?> GetUserPlanDetailsAsync(UserPlanId userPlanId, CancellationToken cancellationToken)
    {
        return await Context.Set<UserPlan>()
                            .Include(up => up.User)
                            .Include(up => up.Plan)
                            .FirstOrDefaultAsync(up => up.Id == userPlanId, cancellationToken);
    }
}
