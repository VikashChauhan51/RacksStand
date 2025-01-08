using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;
using RacksStand.Domain.Enums;

namespace RacksStand.Infrastructure.Data.Configurations;
public class BinConfiguration : IEntityTypeConfiguration<Bin>
{
    public void Configure(EntityTypeBuilder<Bin> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasConversion(
                binId => binId.Value,
                dbId => BinId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(b => b.SelfId)
            .HasConversion(
                selfId => selfId.Value,
                dbId => SelfId.Of(dbId))
            .IsRequired();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(b => b.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(b => b.Label)
            .HasMaxLength(255);

        builder.Property(b => b.Level)
            .HasMaxLength(50);

        builder.Property(b => b.Description)
            .HasMaxLength(500);

        builder.Property(b => b.Barcode)
            .HasConversion(
                barcode => barcode != null ? barcode.Value : null,
                dbValue => dbValue != null ? Barcode.Of(dbValue) : null)
            .HasMaxLength(100);

        builder.Property(c => c.BarcodeFormat)
         .HasConversion(
             unit => unit!=null? unit.ToString():null,
             dbValue => dbValue != null ? Enum.Parse<BarcodeFormat>(dbValue):null)
         .IsRequired();

        builder.Property(b => b.XAxis)
            .HasConversion(
                xAxis => xAxis.Value,
                dbValue => XAxis.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.YAxis)
            .HasConversion(
                yAxis => yAxis.Value,
                dbValue => YAxis.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.Height)
            .HasConversion(
                height => height.Value,
                dbValue => Height.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.Width)
            .HasConversion(
                width => width.Value,
                dbValue => Width.Of(dbValue))
            .IsRequired();

        builder.Property(b => b.Length)
            .HasConversion(
                length => length.Value,
                dbValue => Length.Of(dbValue))
            .IsRequired();

        builder.Property(rack => rack.DiminsionUnit)
             .HasConversion(
                id => id.ToString(),
                dbId => Enum.Parse<DimensionUnit>(dbId))
            .IsRequired();

        builder.Property(rack => rack.State)
            .HasConversion(
               id => id.ToString(),
               dbId => Enum.Parse<BinState>(dbId))
           .IsRequired();

        builder.Property(a => a.ConcurrencyStamp)
            .IsRequired()
            .HasMaxLength(50)
            .IsConcurrencyToken();

        // Relationships
        builder.HasOne(b => b.Self)
            .WithMany()
            .HasForeignKey(b => b.SelfId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => b.Name)
            .HasDatabaseName("IX_Bin_Name");
        builder.HasIndex(b => b.Code)
            .HasDatabaseName("IX_Bin_Code");
        builder.HasIndex(b => b.Label)
            .HasDatabaseName("IX_Bin_Label");
        builder.HasIndex(b => b.Level)
            .HasDatabaseName("IX_Bin_Level");
        builder.HasIndex(b => b.Barcode)
           .HasDatabaseName("IX_Bin_Barcode");
    }
}
