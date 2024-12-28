using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Entities;
public class Rack : Entity<RackId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }
    public required ShopId StoreId { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public virtual ICollection<RackRow> RackRows { get; set; } = [];
}
