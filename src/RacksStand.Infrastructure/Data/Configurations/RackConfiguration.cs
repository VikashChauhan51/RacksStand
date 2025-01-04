using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;


public class RackConfiguration : IEntityTypeConfiguration<Rack>
{
    public void Configure(EntityTypeBuilder<Rack> builder)
    {
        // Configure the primary key
        builder.HasKey(rack => rack.Id);

        builder.Property(rack => rack.Id)
            .HasConversion(
                id => id.Value,
                dbId => RackId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(rack => rack.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(rack => rack.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(rack => rack.Label)
            .HasMaxLength(255)
            .IsRequired(false);

        builder.Property(rack => rack.Description)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(rack => rack.XAxis)
            .IsRequired();

        builder.Property(rack => rack.YAxis)
            .IsRequired();

        builder.Property(rack => rack.Height)
            .IsRequired();

        builder.Property(rack => rack.Width)
            .IsRequired();

        builder.Property(rack => rack.Length)
            .IsRequired();

        builder.Property(rack => rack.DiminsionUnit)
            .IsRequired();

        builder.Property(rack => rack.WarehouseId)
            .HasConversion(
                warehouseId => warehouseId.Value,
                dbId => WarehouseId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        // Configure relationships
        builder.HasOne(rack => rack.Warehouse)
            .WithMany()
            .HasForeignKey(rack => rack.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(rack => rack.Shelves)
            .WithOne()
            .HasForeignKey(shelf => shelf.RackId) 
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(rack => rack.WarehouseId)
            .HasDatabaseName("IX_Rack_WarehouseId");

        builder.HasIndex(rack => rack.Code)
            .HasDatabaseName("IX_Rack_Code");
    }
}

