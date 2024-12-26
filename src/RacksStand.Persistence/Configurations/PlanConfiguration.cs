using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Persistence.Entities;

namespace RacksStand.Infrastructure.Persistence.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Description)
               .HasMaxLength(500);

        builder.Property(p => p.Price)
               .IsRequired()
               .HasPrecision(18, 2);

        builder.Property(p => p.IsActive)
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(p => p.CreatedAt)
               .IsRequired();

        builder.Property(p => p.UpdatedAt)
               .IsRequired();

        // Indexes
        builder.HasIndex(p => p.Name)
               .IsUnique()
               .HasDatabaseName("IX_Plan_Name");
    }
}

