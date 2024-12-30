using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;
public class PocketConfiguration : IEntityTypeConfiguration<Pocket>
{
    public void Configure(EntityTypeBuilder<Pocket> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => PocketId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(p => p.RackRowId)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => RackRowId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(o => o.State)
          .HasDefaultValue(PocketState.Empty)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<PocketState>(dbStatus))
          .IsRequired()
          .HasMaxLength(50);

        builder.HasOne(p => p.RackRow)
            .WithMany(rr => rr.Pockets)
            .HasForeignKey(p => p.RackRowId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(p => p.RackRowId)
            .HasDatabaseName("IX_Pockets_RackRowId");

        builder.HasIndex(p => p.Code)
            .HasDatabaseName("IX_Pockets_Code");

        builder.HasIndex(us => new { us.RackRowId, us.Code })
          .IsUnique()
          .HasDatabaseName("IX_Pockets_RackRowId_Code");

        builder.HasIndex(p => p.State)
          .HasDatabaseName("IX_Pockets_State");
    }
}

