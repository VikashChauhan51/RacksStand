using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        // Configure the primary key
        builder.HasKey(urt => urt.Id);

        builder.Property(urt => urt.Id)
            .HasConversion(
                id => id.Value,
                dbId => UserRefreshTokenId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(urt => urt.UserId)
            .IsRequired();

        builder.Property(urt => urt.Token)
            .IsRequired()
            .HasMaxLength(1000); 

        builder.Property(urt => urt.ExpirationDate)
            .IsRequired();

        builder.Property(urt => urt.RevokedAt)
            .IsRequired(false);

        // Configure relationships
        builder.HasOne(urt => urt.User)
            .WithMany(u => u.UserRefreshTokens) 
            .HasForeignKey(urt => urt.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(urt => new { urt.UserId, urt.Token })
            .IsUnique()
            .HasDatabaseName("IX_UserRefreshToken_UserId_Token");
    }
}

