using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class CompanySubscriptionConfiguration : IEntityTypeConfiguration<CompanySubscription>
{
    public void Configure(EntityTypeBuilder<CompanySubscription> builder)
    {
        builder.HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id)
            .HasConversion(
                subscriptionId => subscriptionId.Value,
                dbId => CompanySubscriptionId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(cs => cs.CompanyId)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cs => cs.PlanId)
            .HasConversion(
                planId => planId.Value,
                dbId => PlanId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        // Configure other properties
        builder.Property(cs => cs.StartDate)
            .IsRequired();

        builder.Property(cs => cs.EndDate)
            .IsRequired();

        builder.Property(cs => cs.IsTrial)
            .IsRequired();

        builder.Property(cs => cs.RenewedAt)
            .IsRequired(false);

        // Configure relationships
        builder.HasOne(cs => cs.Plan)
            .WithMany()
            .HasForeignKey(cs => cs.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cs => cs.Company)
            .WithMany()
            .HasForeignKey(cs => cs.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // For example, indexes
        builder.HasIndex(cs => new { cs.CompanyId, cs.PlanId })
            .HasDatabaseName("IX_CompanySubscription_CompanyId_PlanId");
    }
}
