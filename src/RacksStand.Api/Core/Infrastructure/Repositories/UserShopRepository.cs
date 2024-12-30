using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public class UserShopRepository : Repository<UserShop, UserShopId>, IUserShopRepository
{
    public UserShopRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<UserShop>> GetUserShopsByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        return await Context.Set<UserShop>()
                            .Where(us => us.UserId == userId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UserShop>> GetUserShopsByStoreIdAsync(ShopId storeId, CancellationToken cancellationToken)
    {
        return await Context.Set<UserShop>()
                            .Where(us => us.StoreId == storeId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<UserShop?> GetUserShopDetailsAsync(UserShopId userShopId, CancellationToken cancellationToken)
    {
        return await Context.Set<UserShop>()
                            .Include(us => us.Store)
                            .Include(us => us.User)
                            .FirstOrDefaultAsync(us => us.Id == userShopId, cancellationToken);
    }

    public async Task<IEnumerable<UserShop>> GetUserShopsByRoleAsync(UserRole role, CancellationToken cancellationToken)
    {
        return await Context.Set<UserShop>()
                            .Where(us => us.Role == role)
                            .ToListAsync(cancellationToken);
    }
}
