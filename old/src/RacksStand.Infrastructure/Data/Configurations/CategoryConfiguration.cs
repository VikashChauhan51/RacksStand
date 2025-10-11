using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;
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

        builder.Property(c => c.CompanyId).HasConversion(
                 shopId => shopId.Value,
                 dbId => CompanyId.Of(dbId))
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
        builder.HasIndex(pi => new { pi.Name, pi.CompanyId })
            .IsUnique().HasDatabaseName("IX_Category_Name_CompanyId");

        builder.HasIndex(c => c.CompanyId)
            .HasDatabaseName("IX_Category_CompanyId");

        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Category_Name");
    }
}
