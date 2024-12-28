using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                 planId => planId.Value,
                 dbId => PlanId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Description)
               .HasMaxLength(500);

        builder.Property(p => p.SupportLevel)
              .HasMaxLength(150);

        builder.Property(p => p.Price)
            .HasConversion(
                 planPrice => planPrice.Value,
                 dbId => PlanPrice.Of(dbId))
               .IsRequired()
               .HasPrecision(18, 2);

        builder.HasMany(c => c.PlanFeatures)
           .WithOne(p => p.Plan)
           .HasForeignKey(p => p.PlanId);

        builder.HasMany(p => p.UserPlans)
         .WithOne(up => up.Plan)
         .HasForeignKey(up => up.PlanId);

        // Indexes
        builder.HasIndex(p => p.Name)
               .IsUnique()
               .HasDatabaseName("IX_Plan_Name");
    }
}

