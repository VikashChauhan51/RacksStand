using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;
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

        builder.Property(a => a.EntityType)
            .HasMaxLength(455);
        builder.Property(a => a.EntityId)
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

        builder.Property(a => a.ConcurrencyStamp)
            .IsRequired()
            .HasMaxLength(50)
            .IsConcurrencyToken();
        builder.Property(a => a.CreatedBy)
            .HasMaxLength(455);
        builder.Property(a => a.UpdatedBy)
            .HasMaxLength(455);


        // Indexes
        builder.HasIndex(a => a.Timestamp)
            .HasDatabaseName("IX_Activity_Timestamp");
        builder.HasIndex(a => a.Action)
        .HasDatabaseName("IX_Activity_Action");

    }
}
