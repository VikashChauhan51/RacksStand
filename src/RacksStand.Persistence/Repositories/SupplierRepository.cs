using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Repositories;
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

