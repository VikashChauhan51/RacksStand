using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Category : Entity<CategoryId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required CompanyId CompanyId { get; set; }

    // Navigation properties
    public virtual Company Company { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = [];
}
