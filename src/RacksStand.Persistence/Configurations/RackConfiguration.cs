using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class RackConfiguration : IEntityTypeConfiguration<Rack>
{
    public void Configure(EntityTypeBuilder<Rack> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                rackId => rackId.Value,
                dbId => RackId.Of(dbId))
             .ValueGeneratedNever();

        builder.Property(r => r.StoreId)
          .HasConversion(
              storeId => storeId.Value,
              dbId => ShopId.Of(dbId))
          .IsRequired();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(255);
        builder.Property(r => r.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Description)
           .HasMaxLength(255);
        builder.Property(r => r.Label)
            .HasMaxLength(50);
    }
}
