using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
using Microsoft.AspNetCore.Identity;
using RacksStand.Persistence.Enums;

namespace RacksStand.Persistence.Configurations;
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
             .ValueGeneratedNever();

        builder.Property(us => us.UserId).IsRequired();

        builder.Property(us => us.StoreId)
          .HasConversion(
              storeId => storeId.Value,
              dbId => ShopId.Of(dbId))
          .IsRequired();

        builder.Property(o => o.Role)
          .HasDefaultValue(UserRole.Viewer)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<UserRole>(dbStatus));

        builder.HasOne<IdentityUser>() // Reference to Microsoft Identity's User
             .WithMany()
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
