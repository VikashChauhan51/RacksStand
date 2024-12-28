using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Features.Shops.Domain.Repositories;
public interface IShopRepository : IRepository<Shop, ShopId>
{
    Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken);
}
