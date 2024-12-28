using RacksStand.Api.Core.Domain.Enums;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Entities;
public class UserInvite : Entity<UserInviteId>
{
    public required ShopId ShopId { get; set; }
    public required string Email { get; set; }
    public required string InvitedByUserId { get; set; }
    public required string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public InviteState Status { get; set; }

    // Navigation properties
    public Shop Shop { get; set; } = null!;
    public User User { get; set; } = null!;
}
