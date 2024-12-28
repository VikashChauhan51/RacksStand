using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Core.Domain.Configurations;
public class RackConfiguration : IEntityTypeConfiguration<Rack>
{
    public void Configure(EntityTypeBuilder<Rack> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                rackId => rackId.Value,
                dbId => RackId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(r => r.StoreId)
          .HasConversion(
              storeId => storeId.Value,
              dbId => ShopId.Of(dbId))
          .IsRequired()
          .HasMaxLength(100);

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

        // Indexes
        builder.HasIndex(r => r.StoreId)
            .HasDatabaseName("IX_Racks_StoreId");

        builder.HasIndex(r => r.Name)
            .HasDatabaseName("IX_Racks_Name");

        builder.HasIndex(r => r.Code)
            .HasDatabaseName("IX_Racks_Code");

        builder.HasIndex(r => r.Label)
            .HasDatabaseName("IX_Racks_Label");
    }
}
