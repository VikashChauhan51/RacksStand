using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        // Configure the primary key
        builder.HasKey(po => po.Id);

        builder.Property(po => po.Id)
            .HasConversion(
                id => id.Value,
                dbId => PurchaseOrderId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(po => po.OrderNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.OrderDate)
            .IsRequired();

        builder.Property(po => po.SupplierId)
            .HasConversion(
                id => id.Value,
                dbId => SupplierId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.WarehouseId)
            .HasConversion(
                id => id.Value,
                dbId => WarehouseId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.DeliveryAddressLine1)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(po => po.DeliveryAddressLine2)
            .HasMaxLength(250);

        builder.Property(po => po.DeliveryState)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.DeliveryCity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.DeliveryCountry)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.DeliveryZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(po => po.Status)
            .HasConversion(
                status => status.ToString(),
                dbValue => Enum.Parse<PurchaseOrderStatus>(dbValue))
            .IsRequired();

        builder.Property(po => po.Notes)
            .HasMaxLength(500);

        // Configure relationships
        builder.HasOne(po => po.Supplier)
            .WithMany()
            .HasForeignKey(po => po.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(po => po.Warehouse)
            .WithMany()
            .HasForeignKey(po => po.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(po => po.Lines)
            .WithOne()
            .HasForeignKey(pol => pol.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(po => po.OrderNumber)
            .HasDatabaseName("IX_PurchaseOrder_OrderNumber");

        builder.HasIndex(po => po.SupplierId)
            .HasDatabaseName("IX_PurchaseOrder_SupplierId");

        builder.HasIndex(po => po.WarehouseId)
            .HasDatabaseName("IX_PurchaseOrder_WarehouseId");
    }
}

