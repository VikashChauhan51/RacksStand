using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
{
    public void Configure(EntityTypeBuilder<CompanyUser> builder)
    {
        // Configure the primary key
        builder.HasKey(cu => cu.Id);

        builder.Property(cu => cu.Id)
            .HasConversion(
                id => id.Value,
                dbId => CompanyUserId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties with conversions
        builder.Property(cu => cu.UserId)
            .HasConversion(
                userId => userId.Value,
                dbId => UserId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cu => cu.CompanyId)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        // Configure relationships
        builder.HasOne(cu => cu.User)
            .WithMany()
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cu => cu.Company)
            .WithMany()
            .HasForeignKey(cu => cu.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(cu => cu.UserId)
            .HasDatabaseName("IX_CompanyUser_UserId");

        builder.HasIndex(cu => cu.CompanyId)
            .HasDatabaseName("IX_CompanyUser_CompanyId");
    }
}

