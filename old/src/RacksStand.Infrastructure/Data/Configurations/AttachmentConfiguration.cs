using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;
public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasConversion(
                attachmentId => attachmentId.Value, 
                dbId => AttachmentId.Of(dbId)) 
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(a => a.ContentType)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Extension)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.Url)
            .HasMaxLength(2000);

        builder.Property(a => a.ConcurrencyStamp)
            .IsRequired()
            .HasMaxLength(50)
            .IsConcurrencyToken();

        builder.HasIndex(a => a.Name)
            .HasDatabaseName("IX_Attachment_Name");
    }
}

