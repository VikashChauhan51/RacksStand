using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.UserShops.Domain.Entities;
using RacksStand.Api.Features.UserShops.Domain.ValueObjects;
using RacksStand.Api.Features.Users.Domain.Enums;

namespace RacksStand.Api.Features.UserShops.Domain.Configurations;
public class UserShopConfiguration : IEntityTypeConfiguration<UserShop>
{
    public void Configure(EntityTypeBuilder<UserShop> builder)
    {
        builder.HasKey(us => new { us.UserId, us.StoreId });
        builder.HasKey(us => us.Id);
        builder.Property(us => us.Id)
            .HasConversion(
                userStoreId => userStoreId.Value,
                dbId => UserShopId.Of(dbId))
             .ValueGeneratedNever()
             .HasMaxLength(100);

        builder.Property(us => us.UserId)
            .IsRequired()
            .HasMaxLength(455);

        builder.Property(us => us.StoreId)
          .HasConversion(
              storeId => storeId.Value,
              dbId => ShopId.Of(dbId))
          .IsRequired()
          .HasMaxLength(100);

        builder.Property(o => o.Role)
          .HasDefaultValue(UserRole.Viewer)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<UserRole>(dbStatus))
          .HasMaxLength(50);

        builder.HasOne(us => us.User)
             .WithMany(u => u.UserStores)
             .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Store)
            .WithMany(s => s.UserStores)
            .HasForeignKey(us => us.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(us => us.UserId)
            .HasDatabaseName("IX_UserShop_UserId");

        builder.HasIndex(us => us.StoreId)
            .HasDatabaseName("IX_UserShop_StoreId");

        builder.HasIndex(us => new { us.UserId, us.StoreId })
            .IsUnique()
            .HasDatabaseName("IX_UserShop_UserId_StoreId");
    }
}
