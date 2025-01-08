using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        // Configure the primary key
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                dbId => SupplierId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(s => s.CompanyId)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.PrefererCommunication)
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.Property(s => s.RepresentiveName)
            .HasMaxLength(200);

        builder.Property(s => s.AddressLine1)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(s => s.AddressLine2)
            .HasMaxLength(250);

        builder.Property(s => s.State)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.ZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.BillingAddressLine1)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(s => s.BillingAddressLine2)
            .HasMaxLength(250);

        builder.Property(s => s.BillingState)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.BillingCity)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.BillingCountry)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.BillingZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.ConcurrencyStamp)
           .IsRequired()
           .HasMaxLength(50)
           .IsConcurrencyToken();

        // Configure relationships
        builder.HasOne(s => s.Company)
            .WithMany(c => c.Suppliers)
            .HasForeignKey(s => s.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure navigation properties
        builder.HasMany(s => s.ProductVariants)
            .WithOne(pv => pv.Supplier)
            .HasForeignKey(pv => pv.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.PurchaseOrders)
            .WithOne(po => po.Supplier)
            .HasForeignKey(po => po.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName("IX_Supplier_Code");
    }
}

