using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Infrastructure.Data.Configurations;

public class ProductVariantOptionConfiguration : IEntityTypeConfiguration<ProductVariantOption>
{
    public void Configure(EntityTypeBuilder<ProductVariantOption> builder)
    {
        // Configure the primary key
        builder.HasKey(pvo => pvo.Id);

        builder.Property(pvo => pvo.Id)
            .HasConversion(
                id => id.Value,
                dbId => ProductVariantOptionId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(pvo => pvo.ProductOptionId)
            .HasConversion(
                id => id.Value,
                dbId => ProductOptionId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pvo => pvo.ProductVariantId)
            .HasConversion(
                id => id.Value,
                dbId => ProductVariantId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pvo => pvo.Value)
            .IsRequired()
            .HasMaxLength(200);

        // Configure relationships
        builder.HasOne(pvo => pvo.ProductVariant)
            .WithMany(pv => pv.ProductVariantOptions)
            .HasForeignKey(pvo => pvo.ProductVariantId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pvo => pvo.ProductOption)
            .WithMany()
            .HasForeignKey(pvo => pvo.ProductOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(pvo => pvo.ProductVariantId)
            .HasDatabaseName("IX_ProductVariantOption_ProductVariantId");

        builder.HasIndex(pvo => pvo.ProductOptionId)
            .HasDatabaseName("IX_ProductVariantOption_ProductOptionId");
    }
}

