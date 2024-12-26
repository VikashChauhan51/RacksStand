using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
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
