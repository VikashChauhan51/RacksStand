using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class UserAccountConfirmationConfiguration : IEntityTypeConfiguration<UserAccountConfirmation>
{
    public void Configure(EntityTypeBuilder<UserAccountConfirmation> builder)
    {
        // Configure the primary key
        builder.HasKey(urt => urt.Id);

        builder.Property(urt => urt.Id)
            .HasConversion(
                id => id.Value,
                dbId => UserAccountConfirmationId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(urt => urt.UserId)
            .HasConversion(
                id => id.Value,
                dbId => UserId.Of(dbId))
            .IsRequired();

        builder.Property(urt => urt.Token)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(urt => urt.ExpiresAt)
            .IsRequired();

        // Configure relationships

        // Configure indexes
        builder.HasIndex(urt => urt.Token)
            .HasDatabaseName("IX_UserAccountConfirmation_UserId_Token");
    }
}
