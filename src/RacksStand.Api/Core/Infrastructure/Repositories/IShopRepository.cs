using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface IShopRepository : IRepository<Shop, ShopId>
{
    Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken);
}
