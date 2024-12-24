using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Supplier: Entity<SupplierId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }

    // Navigation properties
    public ICollection<Product> Products { get; set; } = [];
}
