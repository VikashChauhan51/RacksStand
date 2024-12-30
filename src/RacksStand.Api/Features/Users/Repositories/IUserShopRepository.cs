using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;
using RacksStand.Api.Core.Infrastructure.Repositories;

namespace RacksStand.Api.Features.Users.Repositories;
public interface IUserShopRepository : IRepository<UserShop, UserShopId>
{
    Task<IEnumerable<UserShop>> GetUserShopsByUserIdAsync(string userId, CancellationToken cancellationToken);
    Task<IEnumerable<UserShop>> GetUserShopsByStoreIdAsync(ShopId storeId, CancellationToken cancellationToken);
    Task<UserShop?> GetUserShopDetailsAsync(UserShopId userShopId, CancellationToken cancellationToken);
    Task<IEnumerable<UserShop>> GetUserShopsByRoleAsync(UserRole role, CancellationToken cancellationToken);
}

