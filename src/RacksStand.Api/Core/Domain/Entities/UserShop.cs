using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;

namespace RacksStand.Api.Core.Domain.Entities;
public class UserShop : Entity<UserShopId>
{
    public required string UserId { get; set; }
    public required ShopId StoreId { get; set; }
    public required UserRole Role { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public User User { get; set; } = null!;
}
