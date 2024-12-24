using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Rack : Entity<RackId>
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Label { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<RackRow> RackRows { get; set; } = [];
}
