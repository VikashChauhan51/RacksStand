using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace RacksStand.Persistence.Configurations;
public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                storeId => storeId.Value,
                dbId => ShopId.Of(dbId))
            .ValueGeneratedNever();

        builder.Property(s => s.OwnerId).IsRequired();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(s => s.Description)
            .HasMaxLength(500);

        builder.HasMany(s => s.UserStores)
            .WithOne(us => us.Store)
            .HasForeignKey(us => us.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Racks)
            .WithOne(r => r.Store)
            .HasForeignKey(r => r.StoreId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Categories)
          .WithOne(r => r.Store)
          .HasForeignKey(r => r.ShopId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<IdentityUser>() // Reference to Microsoft Identity's User
         .WithMany()
         .HasForeignKey(us => us.OwnerId);

        // Indexes
        builder.HasIndex(s => s.Name) 
            .HasDatabaseName("IX_Shop_Name");

        builder.HasIndex(s => s.OwnerId) 
            .HasDatabaseName("IX_Shop_OwnerId");

        builder.HasIndex(s => new { s.Name, s.OwnerId })
            .IsUnique()
            .HasDatabaseName("IX_Shop_Name_OwnerId");
    }
}

