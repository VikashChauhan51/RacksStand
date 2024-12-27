using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.Configurations;

namespace RacksStand.Persistence.Contexts;
public class RacksStandContext : IdentityDbContext<User>, IRacksStandContext, IdentityContext
{
    public RacksStandContext(DbContextOptions<RacksStandContext> options) : base(options) { }

    public DbSet<UserPlan> UserPlans { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<UserInvite> UserInvites { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<PlanFeature> PlanFeatures { get; set; }
    public DbSet<Shop> Shops { get; set; } = default!;
    public DbSet<UserShop> UserShops { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Pocket> Pockets { get; set; } = default!;
    public DbSet<PocketItem> PocketItems { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Rack> Racks { get; set; } = default!;
    public DbSet<RackRow> RackRows { get; set; } = default!;
    public DbSet<Supplier> Suppliers { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
        base.OnModelCreating(builder);
    }
}
