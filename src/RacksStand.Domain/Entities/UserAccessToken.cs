using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class UserAccessToken : Entity<UserAccessTokenId>
{
    public required UserId UserId { get; set; }
    public required string Token { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }
    public required string Scope { get; set; }

    public virtual User User { get; set; } = null!;
}
