using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Persistence.Configurations;

namespace RacksStand.Infrastructure.Persistence.Contexts;
public class RacksStandContext: DbContext
{
    public RacksStandContext(DbContextOptions<RacksStandContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Pocket> Pockets { get; set; } = default!;
    public DbSet<PocketItem> PocketItems { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Rack> Racks { get; set; } = default!;
    public DbSet<RackRow> RackRows { get; set; } = default!;
    public DbSet<Supplier> Suppliers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
    }
}
