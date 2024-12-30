using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Features.Shops.Infrastructure.Data.Contexts;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

    public DbSet<Shop> Shops { get; set; } = default!;
    public DbSet<UserShop> UserShops { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Rack> Racks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
