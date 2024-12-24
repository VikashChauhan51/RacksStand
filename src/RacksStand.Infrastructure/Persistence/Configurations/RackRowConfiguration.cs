using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Persistence.Configurations;
public class RackRowConfiguration : IEntityTypeConfiguration<RackRow>
{
    public void Configure(EntityTypeBuilder<RackRow> builder)
    {
        builder.HasKey(rr => rr.Id);
        builder.Property(rr => rr.Id)
            .HasConversion(
                rackRowId => rackRowId.Value,
                dbId => RackRowId.Of(dbId));

        builder.Property(rr => rr.RackId)
            .HasConversion(
                rackId => rackId.Value,
                dbId => RackId.Of(dbId))
            .IsRequired();
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
    }
}

