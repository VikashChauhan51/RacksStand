using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        // Configure the primary key
        builder.HasKey(up => up.Id);

        builder.Property(up => up.Id)
            .HasConversion(
                id => id.Value,
                dbId => UserPermissionId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(up => up.ModulePermissionId)
            .IsRequired();

        builder.Property(up => up.UserId)
            .HasConversion(
                id => id.Value,
                dbId => UserId.Of(dbId))
            .IsRequired();

        // Configure relationships
        builder.HasOne(up => up.User)
            .WithMany(u => u.UserPermissions) 
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(up => new { up.UserId, up.ModulePermissionId })
            .IsUnique()
            .HasDatabaseName("IX_UserPermission_UserId_ModulePermissionId");
    }
}

