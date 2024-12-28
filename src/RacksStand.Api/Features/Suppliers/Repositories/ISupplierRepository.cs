using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;

namespace RacksStand.Api.Features.Suppliers.Repositories;
public interface ISupplierRepository : IRepository<Supplier, SupplierId>
{
    Task<IEnumerable<Supplier>> GetSuppliersByShopIdAsync(ShopId shopId, CancellationToken cancellationToken);
}
