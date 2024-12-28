using RacksStand.Api.Core.Domain.Enums;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Persistence.Repositories;
public interface IUserShopRepository : IRepository<UserShop, UserShopId>
{
    Task<IEnumerable<UserShop>> GetUserShopsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserShop>> GetUserShopsByStoreIdAsync(ShopId storeId, CancellationToken cancellationToken);
    Task<UserShop?> GetUserShopDetailsAsync(UserShopId userShopId, CancellationToken cancellationToken);
    Task<IEnumerable<UserShop>> GetUserShopsByRoleAsync(UserRole role, CancellationToken cancellationToken);
}

