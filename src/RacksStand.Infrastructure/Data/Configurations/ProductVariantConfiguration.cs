using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        // Configure the primary key
        builder.HasKey(pv => pv.Id);

        builder.Property(pv => pv.Id)
            .HasConversion(
                id => id.Value,
                dbId => ProductVariantId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(pv => pv.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(pv => pv.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pv => pv.Sku)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pv => pv.PurchasePrice)
            .HasConversion(
                price => price.Value,
                dbValue => PurchasePrice.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.SellingPrice)
            .HasConversion(
                price => price.Value,
                dbValue => SellingPrice.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.Unit)
            .HasConversion(
                unit => unit.Value,
                dbValue => Unit.Of(dbValue))
            .IsRequired()
            .HasMaxLength(1);

        builder.Property(pv => pv.Height)
            .HasConversion(
                value => value.Value,
                dbValue => Height.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.Width)
            .HasConversion(
                value => value.Value,
                dbValue => Width.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.Length)
            .HasConversion(
                value => value.Value,
                dbValue => Length.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.DiminsionUnit)
            .HasConversion(
                unit => unit.ToString(),
                dbValue => Enum.Parse<DimensionUnit>(dbValue))
            .IsRequired();

        builder.Property(pv => pv.Weight)
            .HasConversion(
                value => value.Value,
                dbValue => Weight.Of(dbValue))
            .IsRequired();

        builder.Property(pv => pv.WeightUnit)
            .HasConversion(
                unit => unit.ToString(),
                dbValue => Enum.Parse<WeightUnit>(dbValue))
            .IsRequired();

        builder.Property(pv => pv.ProductId)
            .HasConversion(
                id => id.Value,
                dbId => ProductId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        // Configure optional properties
        builder.Property(pv => pv.Description)
            .HasMaxLength(1000);

        builder.Property(pv => pv.Barcode)
            .HasConversion(
                barcode => barcode != null ? barcode.Value : null,
                dbValue => dbValue != null ? Barcode.Of(dbValue) : null)
            .HasMaxLength(50);

        builder.Property(pv => pv.SupplierId)
            .HasConversion(
                id => id != null ? id.Value : null,
                dbId => dbId != null ? SupplierId.Of(dbId) : null)
            .HasMaxLength(100);

        builder.Property(pv => pv.ImageId)
            .HasConversion(
                id => id != null ? id.Value : null,
                dbId => dbId != null ? AttachmentId.Of(dbId) : null)
            .HasMaxLength(100);

        // Configure relationships
        builder.HasOne(pv => pv.Product)
            .WithMany(p => p.ProductVariants)
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(pv => pv.ProductVariantOptions)
            .WithOne()
            .HasForeignKey(pvo => pvo.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(pv => pv.Code)
            .HasDatabaseName("IX_ProductVariant_Code")
            .IsUnique();

        builder.HasIndex(pv => pv.Sku)
            .HasDatabaseName("IX_ProductVariant_Sku")
            .IsUnique();

        builder.HasIndex(pv => pv.ProductId)
            .HasDatabaseName("IX_ProductVariant_ProductId");

        builder.HasIndex(pv => pv.Barcode)
          .HasDatabaseName("IX_ProductVariant_Barcode");
    }
}

