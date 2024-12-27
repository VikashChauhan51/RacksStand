using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class Category : Entity<CategoryId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required ShopId ShopId { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = [];
}
