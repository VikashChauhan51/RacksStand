using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Domain.Enums;

namespace RacksStand.Api.Core.Domain.Configurations;
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                paymentId => paymentId.Value,
                dbId => PaymentId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        builder.Property(p => p.UserId)
            .IsRequired()
            .HasMaxLength(455);

        builder.Property(p => p.PlanId)
            .HasConversion(
                planId => planId.Value,
                dbId => PlanId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Amount)
            .HasConversion(
                amount => amount.Value,
                dbId => PaymentAmount.Of(dbId))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.PaymentDate)
            .IsRequired();

        builder.Property(o => o.PaymentMethod)
         .HasDefaultValue(PaymentMethod.None)
         .HasConversion(
             s => s.ToString(),
             dbStatus => Enum.Parse<PaymentMethod>(dbStatus))
         .IsRequired()
           .HasMaxLength(50);

        builder.Property(p => p.TransactionId)
            .HasConversion(
                transactionId => transactionId != null ? transactionId.Value : null,
                dbId => dbId != null ? TransactionId.Of(dbId) : null)
            .IsRequired()
            .HasMaxLength(100);


        builder.Property(o => o.Status)
          .HasDefaultValue(PaymentState.None)
          .HasConversion(
              s => s.ToString(),
              dbStatus => Enum.Parse<PaymentState>(dbStatus))
          .IsRequired()
            .HasMaxLength(50);

        // Navigation
        builder.HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Plan)
            .WithMany()
            .HasForeignKey(p => p.PlanId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(p => p.UserId)
            .HasDatabaseName("IX_Payment_UserId");
        builder.HasIndex(p => p.PlanId)
            .HasDatabaseName("IX_Payment_PlanId");
    }
}

