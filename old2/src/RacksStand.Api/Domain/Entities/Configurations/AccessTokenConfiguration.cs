namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccessTokenConfiguration : IEntityTypeConfiguration<AccessToken>
{
    public void Configure(EntityTypeBuilder<AccessToken> builder)
    {
        builder.ToTable("access_tokens");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserId).IsRequired();
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

        // Global query filter to exclude soft-deleted records
        builder.HasQueryFilter(at => at.DeletedAt == null && at.ExpiresAt > DateTimeOffset.UtcNow);

    }
}
