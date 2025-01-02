using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;

namespace RacksStand.Infrastructure.Data.Contexts;
public class RacksStandContext : DbContext
{
    public RacksStandContext(DbContextOptions<RacksStandContext> options) : base(options) { }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Bin> Bins { get; set; }
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Company> Companies { get; set; } = default!;
    public DbSet<CompanySubscription> CompanySubscriptions { get; set; } = default!;
    public DbSet<CompanySubscriptionPayment> CompanySubscriptionPayments { get; set; } = default!;
    public DbSet<CompanyUser> CompanyUsers { get; set; } = default!;
    public DbSet<CompanyUserInvite> CompanyUserInvites { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Feature> Features { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Module> Modules { get; set; }
    public DbSet<ModulePermission> Permissions { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<PlanFeature> PlanFeatures { get; set; }
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductOption> ProductOptions { get; set; } = default!;
    public DbSet<ProductVariant> ProductVariants { get; set; } = default!;
    public DbSet<ProductVariantOption> ProductVariantOptions { get; set; } = default!;
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; } = default!;
    public DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; } = default!;
    public DbSet<Rack> Racks { get; set; } = default!;
    public DbSet<SaleOrder> SaleOrders { get; set; } = default!;
    public DbSet<SaleOrderLine> SaleOrderLines { get; set; } = default!;
    public DbSet<Self> Shelves { get; set; } = default!;
    public DbSet<Supplier> Suppliers { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<UserAccessToken> UserAccessTokens { get; set; } = default!;
    public DbSet<UserPermission> UserPermissions { get; set; } = default!;
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RacksStandContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
