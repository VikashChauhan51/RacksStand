using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class RackRow: Entity<RackRowId>
{
    public required RackId RackId { get; set; }
    public required string Code { get; set; }
    public int RowNumber { get; set; }
    public required string Label { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public virtual Rack Rack { get; set; } = default!;
    public virtual ICollection<Pocket> Pockets { get; set; } = [];
}
