using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Product : Entity<ProductId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public string? Type { get; set; }
    public string? Tags { get; set; }
    public required CategoryId CategoryId { get; set; }
    public required CompanyId CompanyId { get; set; }


    public string? Metadata { get; set; }

    // Navigation properties
    public virtual Category Category { get; set; } = default!;
    public virtual Company Company { get; set; } = default!;
    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = [];
    public virtual ICollection<ProductOption> ProductOptions { get; set; } = [];

}
