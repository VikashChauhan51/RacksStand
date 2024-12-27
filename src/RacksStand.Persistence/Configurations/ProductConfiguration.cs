using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId))
             .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.Sku)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(p => p.Unit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.CategoryId)
            .HasConversion(
                productId => productId.Value,
                dbId => CategoryId.Of(dbId))
            .IsRequired();

        builder.Property(p => p.PurchasePrice)
            .HasConversion(
                purchasePrice => purchasePrice.Value,
                dbId => PurchasePrice.Of(dbId))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.SellingPrice)
            .HasConversion(
                sellingPrice => sellingPrice.Value,
                dbId => SellingPrice.Of(dbId))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.Unit)
           .HasConversion(
               unit => unit.Value,
               dbId => Unit.Of(dbId))
           .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        builder.Property(p => p.SupplierId)
          .HasConversion(
              supplierId => supplierId != null ? supplierId.Value : null,
              dbId => dbId != null ? SupplierId.Of(dbId) : null);

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId);

        // Indexes
        builder.HasIndex(pf => new { pf.CategoryId, pf.Name })
              .IsUnique()
              .HasDatabaseName("IX_Products_Name_CategoryId");

        builder.HasIndex(p => p.Name)
            .HasDatabaseName("IX_Products_Name");

        builder.HasIndex(p => p.Code)
            .HasDatabaseName("IX_Products_Code");

        builder.HasIndex(p => p.Sku)
           .HasDatabaseName("IX_Products_Sku");

        builder.HasIndex(p => p.CategoryId)
            .HasDatabaseName("IX_Products_CategoryId");

        builder.HasIndex(p => p.SupplierId)
            .HasDatabaseName("IX_Products_SupplierId");
    }
}

