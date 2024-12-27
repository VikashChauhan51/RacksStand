namespace RacksStand.Models;
public record UserInviteModel
(
    string Id,
    string ShopId,
    string Email,
    string InvitedByUserId,
    string Token,
    DateTime ExpiryDate,
    string Status
);
