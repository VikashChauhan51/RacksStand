using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class RackRow: Entity<RackRowId>
{
    public required RackId RackId { get; set; }
    public required string Code { get; set; }
    public int RowNumber { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public Rack Rack { get; set; } = default!;
    public ICollection<Pocket> Pockets { get; set; } = [];
}
