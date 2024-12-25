using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace RacksStand.Infrastructure.Persistence.Configurations;
public class UserShopConfiguration : IEntityTypeConfiguration<UserShop>
{
    public void Configure(EntityTypeBuilder<UserShop> builder)
    {
        builder.HasKey(us => new { us.UserId, us.StoreId });
        builder.HasKey(us => us.Id);
        builder.Property(us => us.Id)
            .HasConversion(
                userStoreId => userStoreId.Value,
                dbId => UserShopId.Of(dbId));

        builder.Property(us => us.UserId).IsRequired();

        builder.Property(us => us.StoreId)
            .HasConversion(
                storeId => storeId.Value,
                dbId => ShopId.Of(dbId))
            .IsRequired();

        builder.Property(us => us.Role)
            .IsRequired()
            .HasConversion<string>();

        builder.HasOne<IdentityUser>() // Reference to Microsoft Identity's User
             .WithMany()
             .HasForeignKey(us => us.UserId);

        builder.HasOne(us => us.Store)
            .WithMany(s => s.UserStores)
            .HasForeignKey(us => us.StoreId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
