using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;
using RacksStand.Domain.Enums;

namespace RacksStand.Persistence.Configurations;
public class PocketConfiguration : IEntityTypeConfiguration<Pocket>
{
    public void Configure(EntityTypeBuilder<Pocket> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => PocketId.Of(dbId))
             .ValueGeneratedNever();

        builder.Property(p => p.RackRowId)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => RackRowId.Of(dbId))
            .IsRequired();

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.State)
          .HasDefaultValue(PocketState.Empty)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<PocketState>(dbStatus));

        builder.HasOne(p => p.RackRow)
            .WithMany(rr => rr.Pockets)
            .HasForeignKey(p => p.RackRowId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}

