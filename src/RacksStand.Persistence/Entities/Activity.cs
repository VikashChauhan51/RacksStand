using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class Activity : Entity<ActivityId>
{
    public string? UserId { get; set; }
    public string Action { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Metadata { get; set; }
    public DateTime Timestamp { get; set; }

    // Relationships
    public User User { get; set; } = null!;
}
