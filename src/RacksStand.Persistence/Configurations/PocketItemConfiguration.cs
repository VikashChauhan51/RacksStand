using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Persistence.Configurations;
public class PocketItemConfiguration : IEntityTypeConfiguration<PocketItem>
{
    public void Configure(EntityTypeBuilder<PocketItem> builder)
    {
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id)
            .HasConversion(
                pocketItemId => pocketItemId.Value,
                dbId => PocketItemId.Of(dbId))
             .ValueGeneratedNever();

        builder.Property(pi => pi.PocketId)
            .HasConversion(
                pocketId => pocketId.Value,
                dbId => PocketId.Of(dbId))
            .IsRequired();
        builder.Property(pi => pi.ProductId)
            .HasConversion(
                productId => productId.Value,
                dbId => ProductId.Of(dbId))
            .IsRequired();

        builder.HasOne(pi => pi.Pocket)
            .WithMany(p => p.PocketItems)
            .HasForeignKey(pi => pi.PocketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pi => pi.Product)
            .WithMany(pr => pr.PocketItems)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(pi => pi.PocketId)
            .HasDatabaseName("IX_PocketItems_PocketId");

        builder.HasIndex(pi => pi.ProductId)
            .HasDatabaseName("IX_PocketItems_ProductId");

        builder.HasIndex(pi => new { pi.PocketId, pi.ProductId })
            .IsUnique()
            .HasDatabaseName("IX_PocketItems_PocketId_ProductId");
    }
}
