using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Features.Products.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.Suppliers.Domain.ValueObjects;

namespace RacksStand.Api.Features.Suppliers.Domain.Entities;
public class Supplier : Entity<SupplierId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required ShopId ShopId { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = [];
}
