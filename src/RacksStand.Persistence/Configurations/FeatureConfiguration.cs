using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
               .HasConversion(
                 featureId => featureId.Value,
                 dbId => FeatureId.Of(dbId))
             .ValueGeneratedNever();

        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(f => f.Code)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(f => f.Description)
               .HasMaxLength(500);

        builder.HasMany(c => c.PlanFeatures)
           .WithOne(p => p.Feature)
           .HasForeignKey(p => p.FeatureId);

        // Indexes
        builder.HasIndex(f => f.Name)
               .IsUnique()
               .HasDatabaseName("IX_Feature_Name");

        builder.HasIndex(f => f.Code)
              .IsUnique()
              .HasDatabaseName("IX_Feature_Code");
    }
}

