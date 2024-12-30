using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Entities;
public class Supplier : Entity<SupplierId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required ShopId ShopId { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = [];
}
