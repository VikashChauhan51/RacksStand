namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("roles");
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
        builder.Property(u => u.Name).HasMaxLength(256);
        builder.Property(u => u.NormalizedName).HasMaxLength(256);
        builder.HasMany<IdentityUserRole<string>>()
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
        builder.HasMany<IdentityRoleClaim<string>>()
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();

        builder.HasIndex(r => r.NormalizedName)
       .IsUnique();
        builder.HasIndex(r => r.Name)
            .IsUnique();
    }
}
