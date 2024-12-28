using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Products.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Suppliers.Domain.Entities;
using RacksStand.Api.Features.UserShops.Domain.Entities;

namespace RacksStand.Api.Core.Infrastructure.Contexts;
public interface IRacksStandContext
{
    DbSet<UserPlan> UserPlans { get; }
    DbSet<Payment> Payments { get; }
    DbSet<UserInvite> UserInvites { get; }
    DbSet<Plan> Plans { get; }
    DbSet<Feature> Features { get; }
    DbSet<PlanFeature> PlanFeatures { get; }
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
