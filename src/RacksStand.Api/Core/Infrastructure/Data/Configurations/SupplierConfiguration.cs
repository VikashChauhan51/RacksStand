using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                supplierId => supplierId.Value,
                dbId => SupplierId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.ShopId).HasConversion(
                 shopId => shopId.Value,
                 dbId => ShopId.Of(dbId))
             .IsRequired()
             .HasMaxLength(100);

        // Indexes

        builder.HasIndex(pi => new { pi.Name, pi.ShopId })
           .IsUnique().HasDatabaseName("IX_Suppliers_Name_ShopId");

        builder.HasIndex(s => s.Name)
            .HasDatabaseName("IX_Suppliers_Name");

        builder.HasIndex(s => s.Code)
            .HasDatabaseName("IX_Suppliers_Code");
    }
}
