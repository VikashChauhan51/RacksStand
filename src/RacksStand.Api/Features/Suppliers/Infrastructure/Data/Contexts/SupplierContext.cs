using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Features.Suppliers.Infrastructure.Data.Contexts;

public class SupplierContext : DbContext
{
    public SupplierContext(DbContextOptions<SupplierContext> options) : base(options) { }

    public DbSet<Supplier> Suppliers { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
}
