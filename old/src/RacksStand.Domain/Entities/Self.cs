using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Self : Entity<SelfId>
{
    public required RackId RackId { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Label { get; set; }
    public string? Level { get; set; }
    public string? Description { get; set; }
    public required XAxis XAxis { get; set; }
    public required YAxis YAxis { get; set; }
    public required Height Height { get; set; }
    public required Width Width { get; set; }
    public required Length Length { get; set; }
    public DimensionUnit DiminsionUnit { get; set; }
    public SelfState State { get; set; }

    public virtual Rack Rack { get; set; } = default!;
    public virtual ICollection<Bin> Bins { get; set; } = [];
}
