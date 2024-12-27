using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class RackRowConfiguration : IEntityTypeConfiguration<RackRow>
{
    public void Configure(EntityTypeBuilder<RackRow> builder)
    {
        builder.HasKey(rr => rr.Id);
        builder.Property(rr => rr.Id)
            .HasConversion(
                rackRowId => rackRowId.Value,
                dbId => RackRowId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(rr => rr.RackId)
           .HasConversion(
               rackId => rackId.Value,
               dbId => RackId.Of(dbId))
           .IsRequired()
           .HasMaxLength(100);

        builder.Property(rr => rr.Code)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(rr => rr.Label)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(rr => rr.Rack)
            .WithMany(r => r.RackRows)
            .HasForeignKey(rr => rr.RackId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(rr => rr.RackId)
            .HasDatabaseName("IX_RackRows_RackId");

        builder.HasIndex(rr => rr.Code)
            .HasDatabaseName("IX_RackRows_Code");

        builder.HasIndex(rr => rr.Label)
            .HasDatabaseName("IX_RackRows_Label");
    }
}

