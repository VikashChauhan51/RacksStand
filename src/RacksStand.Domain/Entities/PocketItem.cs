using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class PocketItem:Entity<PocketItemId>
{
    public required PocketId PocketId { get; set; }
    public required ProductId ProductId { get; set; }
    public int Quantity { get; set; }

    // Navigation properties
    public Pocket Pocket { get; set; } = default!;
    public Product Product { get; set; } = default!;

}
