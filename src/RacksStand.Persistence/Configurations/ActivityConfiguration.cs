using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
           .HasConversion(
                activityId => activityId.Value,
                dbId => ActivityId.Of(dbId))
          .ValueGeneratedNever()
              .HasMaxLength(100);

        builder.Property(a => a.UserId)
            .IsRequired()
            .HasMaxLength(455);

        builder.Property(a => a.Action)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Description)
            .HasMaxLength(500);

        builder.Property(a => a.Timestamp)
            .IsRequired();

        builder.Property(a => a.Metadata)
            .HasMaxLength(2000);

        // Navigation
        builder.HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(a => a.Timestamp)
            .HasDatabaseName("IX_Activity_Timestamp");
        builder.HasIndex(a => a.Action)
        .HasDatabaseName("IX_Activity_Action");
        builder.HasIndex(a => a.UserId)
            .HasDatabaseName("IX_Activity_UserId");
    }
}
