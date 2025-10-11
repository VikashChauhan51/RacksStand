using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class SaleOrderLineConfiguration : IEntityTypeConfiguration<SaleOrderLine>
{
    public void Configure(EntityTypeBuilder<SaleOrderLine> builder)
    {
        // Configure the primary key
        builder.HasKey(line => line.Id);

        builder.Property(line => line.Id)
            .HasConversion(
                id => id.Value,
                dbId => SaleOrderLineId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(line => line.SaleOrderId)
            .HasConversion(
                orderId => orderId.Value,
                dbId => SaleOrderId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(line => line.ProductVariantId)
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
                dbId => SellingPrice.Of(dbId))
            .IsRequired();

        builder.Property(pol => pol.CurrencyCode)
            .HasConversion(
                unit => unit.ToString(),
                dbId => Enum.Parse<CurrencyCode>(dbId))
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(a => a.ConcurrencyStamp)
           .IsRequired()
           .HasMaxLength(50)
           .IsConcurrencyToken();

        builder.Ignore(pol => pol.TotalPrice);

        // Configure relationships
        builder.HasOne(line => line.SaleOrder)
            .WithMany(order => order.Lines)
            .HasForeignKey(line => line.SaleOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(line => line.ProductVariant)
            .WithMany()
            .HasForeignKey(line => line.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(line => line.SaleOrderId)
            .HasDatabaseName("IX_SaleOrderLine_SaleOrderId");

        builder.HasIndex(line => line.ProductVariantId)
            .HasDatabaseName("IX_SaleOrderLine_ProductVariantId");
    }
}

