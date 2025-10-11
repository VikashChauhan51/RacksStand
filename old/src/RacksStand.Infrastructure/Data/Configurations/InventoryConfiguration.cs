using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        // Configure the primary key
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasConversion(
                id => id.Value,
                dbId => InventoryId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties with conversions
        builder.Property(i => i.ProductVariantId)
            .HasConversion(
                id => id.Value,
                dbId => ProductVariantId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.BinId)
            .HasConversion(
                id => id.Value,
                dbId => BinId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Quantity)
            .HasConversion(
                qty => qty.Value,
                dbQty => Quantity.Of(dbQty))
            .IsRequired();

        builder.Property(i => i.WarehouseId)
            .HasConversion(
                id => id.Value,
                dbId => WarehouseId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.SupplierId)
            .HasConversion(
                id => id != null ? id.Value : null,
                dbId => dbId != null ? SupplierId.Of(dbId) : null)
            .IsRequired(false)
            .HasMaxLength(100);

        // Configure relationships
        builder.HasOne(i => i.ProductVariant)
            .WithMany()
            .HasForeignKey(i => i.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Bin)
            .WithMany()
            .HasForeignKey(i => i.BinId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Warehouse)
            .WithMany()
            .HasForeignKey(i => i.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Supplier)
            .WithMany()
            .HasForeignKey(i => i.SupplierId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configure indexes
        builder.HasIndex(i => i.ProductVariantId)
            .HasDatabaseName("IX_Inventory_ProductVariantId");

        builder.HasIndex(i => i.BinId)
            .HasDatabaseName("IX_Inventory_BinId");

        builder.HasIndex(i => i.WarehouseId)
            .HasDatabaseName("IX_Inventory_WarehouseId");

        builder.HasIndex(i => i.SupplierId)
            .HasDatabaseName("IX_Inventory_SupplierId");
    }
}
