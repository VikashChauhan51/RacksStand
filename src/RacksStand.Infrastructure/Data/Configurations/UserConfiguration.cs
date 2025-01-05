using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configure the primary key
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                id => id.Value,
                dbId => UserId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(500); 

        builder.Property(u => u.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15); 

        // Configure optional properties
        builder.Property(u => u.LockoutEnd);

        builder.Property(u => u.PhoneNumberConfirmed)
            .HasDefaultValue(false);

        builder.Property(u => u.IsEmailConfirmed)
            .HasDefaultValue(false);

        builder.Property(u => u.LockoutEnabled)
            .HasDefaultValue(false);

        builder.Property(u => u.AccessFailedCount)
            .HasDefaultValue(0);

        // Configure relationships
        builder.HasMany(u => u.UserPermissions)
            .WithOne(up => up.User)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Companies)
            .WithOne(cu => cu.User)
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX_User_Email");

        builder.HasIndex(u => u.UserName)
            .IsUnique()
            .HasDatabaseName("IX_User_UserName");
    }
}

