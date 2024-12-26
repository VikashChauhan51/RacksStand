using RacksStand.Persistence.Enums;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class Pocket: Entity<PocketId>
{
    public required RackRowId RackRowId { get; set; }
    public required string Code { get; set; }
    public int ColumnNumber { get; set; }
    public int? Capacity { get; set; }
    public PocketState State { get; set; }

    // Navigation properties
    public virtual RackRow RackRow { get; set; } = default!;
    public virtual ICollection<PocketItem> PocketItems { get; set; } = [];
}
