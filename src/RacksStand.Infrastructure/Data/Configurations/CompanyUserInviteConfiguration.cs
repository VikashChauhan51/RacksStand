using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class CompanyUserInviteConfiguration : IEntityTypeConfiguration<CompanyUserInvite>
{
    public void Configure(EntityTypeBuilder<CompanyUserInvite> builder)
    {
        // Configure the primary key
        builder.HasKey(cui => cui.Id);

        builder.Property(cui => cui.Id)
            .HasConversion(
                id => id.Value,
                dbId => CompanyUserInviteId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties with conversions
        builder.Property(cui => cui.CompanyId)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(cui => cui.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(cui => cui.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(cui => cui.ExpiryDate)
            .IsRequired();

        builder.Property(cui => cui.Status)
            .HasConversion(
                status => status.ToString(),
                dbValue => Enum.Parse<InviteState>(dbValue))
            .IsRequired();

        // Configure relationships
        builder.HasOne(cui => cui.Company)
            .WithMany()
            .HasForeignKey(cui => cui.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(cui => cui.Email)
            .HasDatabaseName("IX_CompanyUserInvite_Email");

        builder.HasIndex(cui => cui.Token)
            .HasDatabaseName("IX_CompanyUserInvite_Token")
            .IsUnique();

        builder.HasIndex(cui => cui.CompanyId)
            .HasDatabaseName("IX_CompanyUserInvite_CompanyId");
    }
}
