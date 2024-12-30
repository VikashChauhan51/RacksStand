using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Api.Features.Suppliers.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Suppliers.Repositories;
public class SupplierRepository : Repository<Supplier, SupplierId>, ISupplierRepository
{
    public SupplierRepository(SupplierContext context) : base(context) { }

    public async Task<IEnumerable<Supplier>> GetSuppliersByShopIdAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Supplier>()
                            .Where(s => s.ShopId == shopId)
                            .ToListAsync(cancellationToken);
    }
}

