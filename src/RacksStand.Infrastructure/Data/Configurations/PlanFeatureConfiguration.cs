using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;

public class PlanFeatureConfiguration : IEntityTypeConfiguration<PlanFeature>
{
    public void Configure(EntityTypeBuilder<PlanFeature> builder)
    {
        builder.HasKey(pf => pf.Id);

        builder.Property(pf => pf.Id)
               .HasConversion(
                 planFeatureId => planFeatureId.Value,
                 dbId => PlanFeatureId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(pf => pf.PlanId)
                .HasConversion(
                 planId => planId.Value,
                 dbId => PlanId.Of(dbId))
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(pf => pf.FeatureId)
            .HasConversion(
                 featureId => featureId.Value,
                 dbId => FeatureId.Of(dbId))
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(pf => pf.Value)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasOne(pf => pf.Plan)
               .WithMany(p => p.PlanFeatures)
               .HasForeignKey(pf => pf.PlanId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pf => pf.Feature)
               .WithMany(f => f.PlanFeatures)
               .HasForeignKey(pf => pf.FeatureId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(pf => new { pf.PlanId, pf.FeatureId })
               .IsUnique()
               .HasDatabaseName("IX_PlanFeature_PlanId_FeatureId");
    }
}

