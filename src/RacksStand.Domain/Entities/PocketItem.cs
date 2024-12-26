using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class PocketItem:Entity<PocketItemId>
{
    public required PocketId PocketId { get; set; }
    public required ProductId ProductId { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public virtual Pocket Pocket { get; set; } = default!;
    public virtual Product Product { get; set; } = default!;

}
