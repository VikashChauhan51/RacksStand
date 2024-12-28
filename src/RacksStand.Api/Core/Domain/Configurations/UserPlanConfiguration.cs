using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;


namespace RacksStand.Api.Core.Domain.Configurations;
public class UserPlanConfiguration : IEntityTypeConfiguration<UserPlan>
{
    public void Configure(EntityTypeBuilder<UserPlan> builder)
    {
        builder.HasKey(up => up.Id);

        builder.Property(up => up.Id)
           .HasConversion(
                userPlanId => userPlanId.Value,
                dbId => UserPlanId.Of(dbId))
           .HasMaxLength(100);

        builder.Property(up => up.UserId)
            .IsRequired()
            .HasMaxLength(455);

        builder.Property(up => up.PlanId)
            .HasConversion(
                planId => planId.Value,
                dbId => PlanId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(up => up.StartDate)
            .IsRequired();

        builder.Property(up => up.EndDate)
            .IsRequired(false);

        builder.Property(up => up.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Navigation
        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPlans)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Plan)
            .WithMany(p => p.UserPlans)
            .HasForeignKey(up => up.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(up => new { up.UserId, up.PlanId, up.IsActive })
            .IsUnique()
            .HasDatabaseName("IX_UserPlan_UserId_PlanId_IsActive");
    }
}
