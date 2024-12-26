using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Category: Entity<CategoryId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = [];
}
