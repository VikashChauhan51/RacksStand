using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.Enums;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class UserInviteConfiguration : IEntityTypeConfiguration<UserInvite>
{
    public void Configure(EntityTypeBuilder<UserInvite> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.Property(ui => ui.Id)
               .HasConversion(
                userInviteId => userInviteId.Value,
                dbId => UserInviteId.Of(dbId))
            .ValueGeneratedNever();

        builder.Property(ui => ui.ShopId)
            .HasConversion(
                shopId => shopId.Value,
                dbId => ShopId.Of(dbId))
               .IsRequired();

        builder.Property(ui => ui.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(ui => ui.InvitedByUserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(ui => ui.Token)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(o => o.Status)
          .HasDefaultValue(InviteState.Pending)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<InviteState>(dbStatus));

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

