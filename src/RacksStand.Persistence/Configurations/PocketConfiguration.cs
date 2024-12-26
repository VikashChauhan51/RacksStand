using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class PocketConfiguration : IEntityTypeConfiguration<Pocket>
{
    public void Configure(EntityTypeBuilder<Pocket> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => PocketId.Of(dbId));

        builder.Property(p => p.RackRowId)
            .HasConversion(
                rackRowId => rackRowId.Value,
                dbId => RackRowId.Of(dbId))
            .IsRequired();

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(p => p.RackRow)
            .WithMany(rr => rr.Pockets)
            .HasForeignKey(p => p.RackRowId)
            .OnDelete(DeleteBehavior.Cascade); 

        builder.Property(p => p.State)
            .HasConversion<string>(); 
    }
}

