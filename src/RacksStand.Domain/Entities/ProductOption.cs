using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class ProductOption:Entity<ProductOptionId>
{
    public required string Name { get; set; }
    public required ProductId ProductId { get; set; }
}
