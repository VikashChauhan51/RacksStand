using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Rack : Entity<RackId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public required XAxis XAxis { get; set; }
    public required YAxis YAxis { get; set; }
    public required Height Height { get; set; }
    public required Width Width { get; set; }
    public required Length Length { get; set; }
    public DimensionUnit DiminsionUnit { get; set; }
    public required WarehouseId WarehouseId { get; set; }
    public RackState State { get; set; }

    // Navigation properties
    public virtual Warehouse Warehouse { get; set; } = default!;
    public virtual ICollection<Self> Shelves { get; set; } = [];
}
