using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.UserShops.Domain.Entities;

namespace RacksStand.Api.Features.Shops.Domain.Entities;
public class Shop : Entity<ShopId>
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string OwnerId { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = default!;
    public virtual ICollection<Category> Categories { get; set; } = [];
    public virtual ICollection<UserShop> UserStores { get; set; } = [];
    public virtual ICollection<Rack> Racks { get; set; } = [];
    public virtual ICollection<UserInvite> UserInvites { get; set; } = [];
}

