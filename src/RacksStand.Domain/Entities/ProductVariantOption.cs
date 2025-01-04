using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class ProductVariantOption : Entity<ProductVariantOptionId>
{
    public required ProductOptionId ProductOptionId { get; set; }
    public required ProductVariantId ProductVariantId { get; set; }
    public required string Value { get; set; }

    public virtual ProductOption ProductOption { get; set; } = null!;
    public virtual ProductVariant ProductVariant { get; set; } = null!;
}
