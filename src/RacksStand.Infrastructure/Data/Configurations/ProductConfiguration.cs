using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Configure the primary key
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                dbId => ProductId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.CategoryId)
            .HasConversion(
                id => id.Value,
                dbId => CategoryId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.CompanyId)
            .HasConversion(
                id => id.Value,
                dbId => CompanyId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        // Configure optional properties
        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.Brand)
            .HasMaxLength(200);

        builder.Property(p => p.Type)
            .HasMaxLength(200);

        builder.Property(p => p.Tags)
            .HasMaxLength(500);

        builder.Property(p => p.Metadata)
            .HasMaxLength(2000);

        // Configure relationships
        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Company)
            .WithMany()
            .HasForeignKey(p => p.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductVariants)
            .WithOne()
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductOptions)
            .WithOne()
            .HasForeignKey(po => po.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(p => p.Name)
            .HasDatabaseName("IX_Product_Name");

        builder.HasIndex(p => new { p.CategoryId, p.CompanyId })
            .HasDatabaseName("IX_Product_CategoryId_CompanyId");
    }
}


