using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class ProductVariantOption : Entity<ProductVariantOptionId>
{
    public required ProductOptionId ProductOptionId { get; set; }
    public required ProductVariantId ProductVariantId { get; set; }
}
