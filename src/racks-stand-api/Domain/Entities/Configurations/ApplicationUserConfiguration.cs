namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(256);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.NormalizedUserName).IsUnique();

        builder.HasMany<IdentityUserRole<string>>()
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();

    }
}
