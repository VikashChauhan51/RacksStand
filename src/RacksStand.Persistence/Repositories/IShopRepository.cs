using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;


namespace RacksStand.Persistence.Repositories;
public interface IShopRepository : IRepository<Shop, ShopId>
{
    Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken);
}
