using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class UserRefreshToken:Entity<UserRefreshTokenId>
{
    public required UserId UserId { get; set; }
    public required string Token { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
