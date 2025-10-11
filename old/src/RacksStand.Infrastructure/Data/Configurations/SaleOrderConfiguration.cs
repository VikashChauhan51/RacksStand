using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
{
    public void Configure(EntityTypeBuilder<SaleOrder> builder)
    {
        // Configure the primary key
        builder.HasKey(order => order.Id);

        builder.Property(order => order.Id)
            .HasConversion(
                id => id.Value,
                dbId => SaleOrderId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(order => order.OrderNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.OrderDate)
            .IsRequired();

        builder.Property(order => order.CustomerId)
            .HasConversion(
                customerId => customerId.Value,
                dbId => CustomerId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.WarehouseId)
            .HasConversion(
                warehouseId => warehouseId.Value,
                dbId => WarehouseId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(order => order.ShippingAddress)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(order => order.ShippingCity)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(order => order.ShippingState)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(order => order.ShippingCountry)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(order => order.ShippingPostalCode)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(order => order.Status)
            .IsRequired();

        builder.Property(order => order.Notes)
            .HasMaxLength(500)
            .IsRequired(false);

        // Configure relationships
        builder.HasOne(order => order.Customer)
            .WithMany()
            .HasForeignKey(order => order.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(order => order.Warehouse)
            .WithMany()
            .HasForeignKey(order => order.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(order => order.Lines)
            .WithOne()
            .HasForeignKey(line => line.SaleOrderId) 
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(order => order.OrderNumber)
            .HasDatabaseName("IX_SaleOrder_OrderNumber");

        builder.HasIndex(order => order.CustomerId)
            .HasDatabaseName("IX_SaleOrder_CustomerId");

        builder.HasIndex(order => order.WarehouseId)
            .HasDatabaseName("IX_SaleOrder_WarehouseId");
    }
}
