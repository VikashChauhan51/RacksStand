using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;
using RacksStand.Domain.Enums;

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
             .HasConversion(
                xAxis => xAxis.Value,
                dbId => XAxis.Of(dbId))
            .IsRequired();

        builder.Property(rack => rack.YAxis)
             .HasConversion(
                yAxis => yAxis.Value,
                dbId => YAxis.Of(dbId))
            .IsRequired();

        builder.Property(rack => rack.Height)
             .HasConversion(
                height => height.Value,
                dbId => Height.Of(dbId))
            .IsRequired();

        builder.Property(rack => rack.Width)
             .HasConversion(
                width => width.Value,
                dbId => Width.Of(dbId))
            .IsRequired();

        builder.Property(rack => rack.Length)
             .HasConversion(
                length => length.Value,
                dbId => Length.Of(dbId))
            .IsRequired();

        builder.Property(rack => rack.DiminsionUnit)
             .HasConversion(
                id => id.ToString(),
                dbId => Enum.Parse<DimensionUnit>(dbId))
            .IsRequired();

        builder.Property(rack => rack.WarehouseId)
            .HasConversion(
                warehouseId => warehouseId.Value,
                dbId => WarehouseId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(rack => rack.State)
            .HasConversion(
               id => id.ToString(),
               dbId => Enum.Parse<RackState>(dbId))
           .IsRequired();

        builder.Property(a => a.ConcurrencyStamp)
            .IsRequired()
            .HasMaxLength(50)
            .IsConcurrencyToken();

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

