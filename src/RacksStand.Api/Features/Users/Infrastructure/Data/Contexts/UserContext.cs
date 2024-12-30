using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Users.Infrastructure.Data.Contexts;

public class UserContext : IdentityDbContext<User>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<UserPlan> UserPlans { get; set; }
    public DbSet<UserInvite> UserInvites { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<PlanFeature> PlanFeatures { get; set; }
    public DbSet<Shop> Shops { get; set; } = default!;
    public DbSet<UserShop> UserShops { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);
        base.OnModelCreating(builder);
    }
}
