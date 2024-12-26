using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class UserInviteConfiguration : IEntityTypeConfiguration<UserInvite>
{
    public void Configure(EntityTypeBuilder<UserInvite> builder)
    {
        builder.HasKey(ui => ui.Id);

        builder.Property(ui => ui.Id)
               .ValueGeneratedOnAdd();

        builder.Property(ui => ui.ShopId)
               .IsRequired();

        builder.Property(ui => ui.Email)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(ui => ui.InvitedByUserId)
               .IsRequired()
               .HasMaxLength(450);

        builder.Property(ui => ui.CreatedAt)
               .IsRequired();

        builder.Property(ui => ui.ExpiryDate)
               .IsRequired();

        builder.Property(ui => ui.IsAccepted)
               .IsRequired()
               .HasDefaultValue(false);

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

