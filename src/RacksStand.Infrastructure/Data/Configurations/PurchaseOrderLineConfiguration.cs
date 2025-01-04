using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
    {
        // Configure the primary key
        builder.HasKey(pol => pol.Id);

        builder.Property(pol => pol.Id)
            .HasConversion(
                id => id.Value,
                dbId => PurchaseOrderLineId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(pol => pol.PurchaseOrderId)
            .HasConversion(
                orderId => orderId.Value,
                dbId => PurchaseOrderId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pol => pol.ProductVariantId)
            .HasConversion(
                variantId => variantId.Value,
                dbId => ProductVariantId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pol => pol.Quantity)
              .HasConversion(
                quantity => quantity.Value,
                dbId => Quantity.Of(dbId))
            .IsRequired();

        builder.Property(pol => pol.UnitPrice)
             .HasConversion(
                price => price.Value,
                dbId => PurchasePrice.Of(dbId))
            .IsRequired();

        builder.Property(pol => pol.Unit)
            .HasConversion(
                unit => unit.Value,
                dbId => Unit.Of(dbId))
            .IsRequired()
            .HasMaxLength(1);

        // Configure the TotalPrice property (calculated)
        builder.Ignore(pol => pol.TotalPrice);

        // Configure relationships
        builder.HasOne(pol => pol.PurchaseOrder)
            .WithMany(po => po.Lines)
            .HasForeignKey(pol => pol.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pol => pol.ProductVariant)
            .WithMany()
            .HasForeignKey(pol => pol.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(pol => pol.PurchaseOrderId)
            .HasDatabaseName("IX_PurchaseOrderLine_PurchaseOrderId");

        builder.HasIndex(pol => pol.ProductVariantId)
            .HasDatabaseName("IX_PurchaseOrderLine_ProductVariantId");
    }
}
