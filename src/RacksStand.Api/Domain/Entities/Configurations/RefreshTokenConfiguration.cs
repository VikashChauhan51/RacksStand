namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserId).IsRequired();
        builder.Property(u => u.TokenHash).IsRequired();
        builder.Property(u => u.ExpiresAt).IsRequired();

        // Application user navigation property configuration
        builder.HasOne(at => at.User)
          .WithMany()
          .HasForeignKey(at => at.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        // indxes
        builder.HasIndex(at => at.UserId);
        builder.HasIndex(at => at.ExpiresAt);
        builder.HasIndex(at => at.DeletedAt);
        builder.HasIndex(rt => rt.TokenHash).IsUnique();

        // Global query filter to exclude soft-deleted records
        builder.HasQueryFilter(at => at.DeletedAt == null && at.ExpiresAt > DateTimeOffset.UtcNow);

    }
}
