using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Configure the primary key
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                dbId => CustomerId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(c => c.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .HasMaxLength(150);

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(c => c.AddressLine1)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(c => c.AddressLine2)
            .HasMaxLength(300);

        builder.Property(c => c.State)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Notes)
            .HasMaxLength(1000);

        // Configure relationships
        builder.HasMany(c => c.SaleOrders)
            .WithOne()
            .HasForeignKey(so => so.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(c => c.Email)
            .HasDatabaseName("IX_Customer_Email");

        builder.HasIndex(c => c.PhoneNumber)
            .HasDatabaseName("IX_Customer_PhoneNumber");
    }
}
