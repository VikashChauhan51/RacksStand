using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class ModulePermissionConfiguration : IEntityTypeConfiguration<ModulePermission>
{
    public void Configure(EntityTypeBuilder<ModulePermission> builder)
    {
        // Configure the primary key
        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.Id)
            .HasConversion(
                id => id.Value,
                dbId => ModulePermissionId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties
        builder.Property(mp => mp.ModuleId)
            .HasConversion(
                id => id.Value,
                dbId => ModuleId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mp => mp.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(mp => mp.Code)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(mp => mp.Description)
            .IsRequired()
            .HasMaxLength(1000);

        // Configure relationships
        builder.HasOne(mp => mp.Module)
            .WithMany(m => m.Permissions)
            .HasForeignKey(mp => mp.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(mp => mp.Code)
            .IsUnique()
            .HasDatabaseName("IX_ModulePermission_Code");
    }
}
