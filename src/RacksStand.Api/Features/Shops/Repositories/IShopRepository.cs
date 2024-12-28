using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;

namespace RacksStand.Api.Features.Shops.Repositories;
public interface IShopRepository : IRepository<Shop, ShopId>
{
    Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken);
}
