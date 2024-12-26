using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class Supplier: Entity<SupplierId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = [];
}
