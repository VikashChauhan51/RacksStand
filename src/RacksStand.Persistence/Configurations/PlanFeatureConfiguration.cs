using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class PlanFeatureConfiguration : IEntityTypeConfiguration<PlanFeature>
{
    public void Configure(EntityTypeBuilder<PlanFeature> builder)
    {
        builder.HasKey(pf => pf.PlanFeatureId);

        builder.Property(pf => pf.PlanFeatureId)
               .ValueGeneratedOnAdd();

        builder.Property(pf => pf.PlanId)
               .IsRequired();

        builder.Property(pf => pf.FeatureId)
               .IsRequired();

        builder.Property(pf => pf.Limit)
               .HasDefaultValue(null);

        builder.Property(pf => pf.CreatedAt)
               .IsRequired();

        builder.Property(pf => pf.UpdatedAt)
               .IsRequired();

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

