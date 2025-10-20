namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Api.Domain.Entities;

public class CacheStoreConfiguration : IEntityTypeConfiguration<CacheStore>
{
    public void Configure(EntityTypeBuilder<CacheStore> builder)
    {
        // Table name
        builder.ToTable("cache_stores");

        // Primary key
        builder.HasKey(c => c.Key);

        // Column constraints
        builder.Property(c => c.Key)
               .IsRequired()
               .HasMaxLength(200); // optional, to prevent unbounded PK

        builder.Property(c => c.Value)
               .IsRequired();

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.ExpiresAt)
               .IsRequired(false);

        // Optional: Index for fast expiration queries
        builder.HasIndex(c => c.ExpiresAt);
    }
}
