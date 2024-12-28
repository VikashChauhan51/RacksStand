using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;

namespace RacksStand.Api.Features.Shops.Domain.Repositories;
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
