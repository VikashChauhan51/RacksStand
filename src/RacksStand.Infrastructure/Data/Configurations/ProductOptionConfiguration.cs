using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.HasKey(po => po.Id);
        builder.Property(po => po.Id)
            .HasConversion(
                id => id.Value,
                value => ProductOptionId.Of(value)
            )
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(po => po.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(po => po.ProductId)
            .HasConversion(
                id => id.Value,
                value => ProductId.Of(value)
            )
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(po => po.Product)
            .WithMany(po => po.ProductOptions)
            .HasForeignKey(po => po.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(line => line.Name)
           .HasDatabaseName("IX_ProductOption_Name");

        builder.HasIndex(line => line.ProductId)
           .HasDatabaseName("IX_ProductOption_ProductId");
    }
}
