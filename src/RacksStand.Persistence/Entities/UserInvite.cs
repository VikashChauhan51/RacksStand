using RacksStand.Persistence.Enums;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Entities;
public class UserInvite : Entity<UserInviteId>
{
    public required ShopId ShopId { get; set; }
    public required string Email { get; set; }
    public required string InvitedByUserId { get; set; }
    public required string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public InviteState Status { get; set; }

    // Navigation property to the associated Shop
    public Shop Shop { get; set; } = null!;
}
