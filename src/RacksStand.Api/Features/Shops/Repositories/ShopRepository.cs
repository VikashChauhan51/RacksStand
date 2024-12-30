using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Api.Features.Shops.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Shops.Repositories;
public class ShopRepository : Repository<Shop, ShopId>, IShopRepository
{
    public ShopRepository(ShopContext context) : base(context) { }

    public async Task<Shop?> GetShopWithDetailsAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Shop>()
                            .Include(s => s.Categories)
                            .Include(s => s.UserStores)
                            .Include(s => s.Racks)
                            .FirstOrDefaultAsync(s => s.Id == shopId, cancellationToken);
    }
}
