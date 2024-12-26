using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.HasKey(f => f.FeatureId);

        builder.Property(f => f.FeatureId)
               .ValueGeneratedOnAdd();

        builder.Property(f => f.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(f => f.Description)
               .HasMaxLength(500);

        builder.Property(f => f.CreatedAt)
               .IsRequired();

        builder.Property(f => f.UpdatedAt)
               .IsRequired();

        // Indexes
        builder.HasIndex(f => f.Name)
               .IsUnique()
               .HasDatabaseName("IX_Feature_Name");
    }
}

