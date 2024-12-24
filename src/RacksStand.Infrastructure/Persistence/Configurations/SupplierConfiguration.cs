using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Persistence.Configurations;
public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                supplierId => supplierId.Value,
                dbId => SupplierId.Of(dbId));

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);
    }
}
