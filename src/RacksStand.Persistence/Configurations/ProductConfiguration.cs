using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId));

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(p => p.Unit)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.CategoryId)
            .HasConversion(
                categoryId => categoryId.Value,
                dbId => CategoryId.Of(dbId))
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
    }
}

