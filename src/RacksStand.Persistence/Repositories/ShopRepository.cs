using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Repositories;
public class ShopRepository : Repository<Shop, ShopId>, IShopRepository
{
    public ShopRepository(DbContext context) : base(context) { }

    public async Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Shop>()
                            .Include(s => s.Categories)
                            .Include(s => s.UserStores)
                            .Include(s => s.Racks)
                            .FirstOrDefaultAsync(s => s.Id == shopId, cancellationToken);
    }
}
