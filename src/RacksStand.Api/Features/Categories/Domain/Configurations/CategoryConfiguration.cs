using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Features.Categories.Domain.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
                 categoryId => categoryId.Value,
                 dbId => CategoryId.Of(dbId))
             .ValueGeneratedNever()
              .HasMaxLength(100);

        builder.Property(c => c.ShopId).HasConversion(
                 shopId => shopId.Value,
                 dbId => ShopId.Of(dbId))
             .IsRequired()
             .HasMaxLength(100);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.Description)
            .HasMaxLength(500);

        builder.HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        // Indexes
        builder.HasIndex(pi => new { pi.Name, pi.ShopId })
            .IsUnique().HasDatabaseName("IX_Category_Name_ShopId");

        builder.HasIndex(c => c.ShopId)
            .HasDatabaseName("IX_Category_ShopId");

        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Category_Name");
    }
}
