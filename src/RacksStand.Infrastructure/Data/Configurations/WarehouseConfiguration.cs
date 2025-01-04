using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        // Configure the primary key
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Id)
            .HasConversion(
                id => id.Value,
                dbId => WarehouseId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(w => w.CompanyId)
            .IsRequired();

        builder.Property(w => w.ManagerId)
            .IsRequired();

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(w => w.Code)
            .IsRequired()
            .HasMaxLength(3); 

        builder.Property(w => w.Description)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(w => w.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(b => b.Height)
            .HasConversion(
                height => height.Value,
                dbValue => Height.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.Width)
            .HasConversion(
                width => width.Value,
                dbValue => Width.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.Length)
            .HasConversion(
                length => length.Value,
                dbValue => Length.Of(dbValue))
            .IsRequired();

        builder.Property(rack => rack.DiminsionUnit)
             .HasConversion(
                id => id.ToString(),
                dbId => Enum.Parse<DimensionUnit>(dbId))
            .IsRequired();

        builder.Property(w => w.AddressLine1)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(w => w.AddressLine2)
            .HasMaxLength(255); 

        builder.Property(w => w.State)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.City)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.Country)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.ZipCode)
            .IsRequired()
            .HasMaxLength(20); 

        builder.Property(w => w.ReturnAddressLine1)
            .IsRequired()
            .HasMaxLength(255); 

        builder.Property(w => w.ReturnAddressLine2)
            .HasMaxLength(255); 

        builder.Property(w => w.ReturnState)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.ReturnCity)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.ReturnCountry)
            .IsRequired()
            .HasMaxLength(100); 

        builder.Property(w => w.ReturnZipCode)
            .IsRequired()
            .HasMaxLength(20); 

        // Configure relationships
        builder.HasOne(w => w.Company)
            .WithMany(c => c.Warehouses)
            .HasForeignKey(w => w.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Manager)
            .WithMany()
            .HasForeignKey(w => w.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure navigation properties
        builder.HasMany(w => w.PurchaseOrders)
            .WithOne(po => po.Warehouse)
            .HasForeignKey(po => po.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.SaleOrders)
            .WithOne(so => so.Warehouse)
            .HasForeignKey(so => so.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(w => w.Racks)
            .WithOne(r => r.Warehouse)
            .HasForeignKey(r => r.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes (if any)
        builder.HasIndex(w => w.Name)
            .IsUnique()
            .HasDatabaseName("IX_Warehouse_Name");

        builder.HasIndex(w => w.Code)
            .HasDatabaseName("IX_Warehouse_Code");
    }
}
