using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                supplierId => supplierId.Value,
                dbId => SupplierId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);

        // Indexes
        builder.HasIndex(s => s.Name)
            .HasDatabaseName("IX_Suppliers_Name");

        builder.HasIndex(s => s.Code)
            .HasDatabaseName("IX_Suppliers_Code");
    }
}
