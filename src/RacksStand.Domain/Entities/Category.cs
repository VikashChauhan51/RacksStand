using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Category: Entity<CategoryId>
{
    public required string Name { get; set; }

    // Navigation properties
    public ICollection<Product> Products { get; set; } = [];
}
