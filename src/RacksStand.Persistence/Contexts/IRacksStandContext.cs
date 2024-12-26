using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;

namespace RacksStand.Persistence.Contexts;
public interface IRacksStandContext
{
    DbSet<Category> Categories { get; }
    DbSet<PocketItem> PocketItems { get; }
    DbSet<Pocket> Pockets { get; }
    DbSet<Product> Products { get; }
    DbSet<RackRow> RackRows { get; }
    DbSet<Rack> Racks { get; }
    DbSet<Shop> Shops { get; }
    DbSet<Supplier> Suppliers { get; }
    DbSet<UserShop> UserShops { get; }
}
