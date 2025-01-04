using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class SelfConfiguration : IEntityTypeConfiguration<Self>
{
    public void Configure(EntityTypeBuilder<Self> builder)
    {
        // Configure the primary key
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                dbId => SelfId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(s => s.RackId)
            .HasConversion(
                rackId => rackId.Value,
                dbId => RackId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(s => s.Label)
            .HasMaxLength(255);

        builder.Property(s => s.Level)
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.XAxis)
            .IsRequired();

        builder.Property(s => s.YAxis)
            .IsRequired();

        builder.Property(s => s.Height)
            .IsRequired();

        builder.Property(s => s.Width)
            .IsRequired();

        builder.Property(s => s.Length)
            .IsRequired();

        builder.Property(s => s.DiminsionUnit)
            .IsRequired();

        // Configure relationships
        builder.HasOne(s => s.Rack)
            .WithMany(r => r.Shelves)
            .HasForeignKey(s => s.RackId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure navigation properties
        builder.HasMany(s => s.Bins)
            .WithOne()
            .HasForeignKey(b => b.SelfId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(s => s.RackId)
            .HasDatabaseName("IX_Self_RackId");

        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName("IX_Self_Code");
    }
}
