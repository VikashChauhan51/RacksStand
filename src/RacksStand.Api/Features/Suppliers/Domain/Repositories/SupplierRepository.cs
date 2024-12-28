using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.Suppliers.Domain.Entities;
using RacksStand.Api.Features.Suppliers.Domain.ValueObjects;

namespace RacksStand.Api.Features.Suppliers.Domain.Repositories;
public class SupplierRepository : Repository<Supplier, SupplierId>, ISupplierRepository
{
    public SupplierRepository(DbContext context) : base(context) { }

    public async Task<IEnumerable<Supplier>> GetSuppliersByShopIdAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Supplier>()
                            .Where(s => s.ShopId == shopId)
                            .ToListAsync(cancellationToken);
    }
}

