using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class UserAccountConfirmation : Entity<UserAccountConfirmationId>
{
    public required UserId UserId { get; set; }
    public required string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
    public virtual User User { get; set; } = null!;
}
