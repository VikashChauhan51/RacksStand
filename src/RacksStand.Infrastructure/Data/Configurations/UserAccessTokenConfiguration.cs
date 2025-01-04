using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class UserAccessTokenConfiguration : IEntityTypeConfiguration<UserAccessToken>
{
    public void Configure(EntityTypeBuilder<UserAccessToken> builder)
    {
        // Configure the primary key
        builder.HasKey(uat => uat.Id);

        builder.Property(uat => uat.Id)
            .HasConversion(
                id => id.Value,
                dbId => UserAccessTokenId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(uat => uat.UserId)
            .IsRequired();

        builder.Property(uat => uat.Token)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(uat => uat.Scope)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(uat => uat.ExpirationDate)
            .IsRequired();

        // Configure optional properties
        builder.Property(uat => uat.RevokedAt);

        // Configure relationships
        builder.HasOne(uat => uat.User)
            .WithMany(u => u.UserAccessTokens) 
            .HasForeignKey(uat => uat.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(uat => uat.Token)
            .IsUnique()
            .HasDatabaseName("IX_UserAccessToken_Token");

        builder.HasIndex(uat => uat.UserId)
            .HasDatabaseName("IX_UserAccessToken_UserId");
    }
}
