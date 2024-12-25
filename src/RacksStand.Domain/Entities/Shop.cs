using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Shop : Entity<ShopId>
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<UserShop> UserStores { get; set; } = [];
    public ICollection<Rack> Racks { get; set; } = [];
}

