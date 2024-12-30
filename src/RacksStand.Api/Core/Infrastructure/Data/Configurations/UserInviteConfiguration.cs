using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;

namespace RacksStand.Api.Core.Infrastructure.Data.Configurations;

public class UserInviteConfiguration : IEntityTypeConfiguration<UserInvite>
{
    public void Configure(EntityTypeBuilder<UserInvite> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.Property(ui => ui.Id)
               .HasConversion(
                userInviteId => userInviteId.Value,
                dbId => UserInviteId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(ui => ui.ShopId)
            .HasConversion(
                shopId => shopId.Value,
                dbId => ShopId.Of(dbId))
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(ui => ui.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(ui => ui.InvitedByUserId)
               .IsRequired()
               .HasMaxLength(455);

        builder.Property(ui => ui.Token)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(o => o.Status)
          .HasDefaultValue(InviteState.Pending)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<InviteState>(dbStatus))
          .HasMaxLength(50);

        builder.HasOne(ui => ui.Shop)
               .WithMany(s => s.UserInvites)
               .HasForeignKey(ui => ui.ShopId)
               .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(ui => new { ui.ShopId, ui.Email })
               .IsUnique()
               .HasDatabaseName("IX_UserInvite_ShopId_Email");

        builder.HasIndex(ui => ui.Email)
               .HasDatabaseName("IX_UserInvite_Email");
    }
}

