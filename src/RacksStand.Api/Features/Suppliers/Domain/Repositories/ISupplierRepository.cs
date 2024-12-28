using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.Suppliers.Domain.Entities;
using RacksStand.Api.Features.Suppliers.Domain.ValueObjects;

namespace RacksStand.Api.Features.Suppliers.Domain.Repositories;
public interface ISupplierRepository : IRepository<Supplier, SupplierId>
{
    Task<IEnumerable<Supplier>> GetSuppliersByShopIdAsync(ShopId shopId, CancellationToken cancellationToken);
}
