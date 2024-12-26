using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class UserShop : Entity<UserShopId>
{
    public required UserId UserId { get; set; }
    public required ShopId StoreId { get; set; }
    public required UserRole Role { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
}
