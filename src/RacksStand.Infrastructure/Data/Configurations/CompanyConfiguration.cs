using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.AddressLine1)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.AddressLine2)
            .HasMaxLength(255);

        builder.Property(c => c.State)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.ZipCode)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.OwnerUserId)
            .HasConversion(
                userId => userId.Value,
                dbId => UserId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.PlanId)
            .HasConversion(
                planId => planId.Value,
                dbId => PlanId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Company_Name");
    }
}

