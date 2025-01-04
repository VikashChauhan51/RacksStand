using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Infrastructure.Data.Configurations;

public class CompanySubscriptionPaymentConfiguration : IEntityTypeConfiguration<CompanySubscriptionPayment>
{
    public void Configure(EntityTypeBuilder<CompanySubscriptionPayment> builder)
    {
        // Configure the primary key
        builder.HasKey(csp => csp.Id);

        builder.Property(csp => csp.Id)
            .HasConversion(
                paymentId => paymentId.Value,
                dbId => CompanySubscriptionPaymentId.Of(dbId))
            .ValueGeneratedNever()
            .HasMaxLength(100);

        // Configure required properties with conversions
        builder.Property(csp => csp.CompanySubscriptionId)
            .HasConversion(
                subscriptionId => subscriptionId.Value,
                dbId => CompanySubscriptionId.Of(dbId))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(csp => csp.Amount)
            .HasConversion(
                amount => amount.Value,
                dbAmount => PaymentAmount.Of(dbAmount))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(csp => csp.PaymentMethod)
            .HasConversion(
                method => method.ToString(),
                dbValue => Enum.Parse<PaymentMethod>(dbValue))
            .IsRequired();

        builder.Property(csp => csp.TransactionId)
            .HasConversion(
                transactionId => transactionId != null ? transactionId.Value : null,
                dbId => dbId != null ? TransactionId.Of(dbId) : null)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(csp => csp.PaymentDate)
            .IsRequired();

        builder.Property(csp => csp.Status)
            .HasConversion(
                status => status.ToString(),
                dbValue => Enum.Parse<PaymentState>(dbValue))
            .IsRequired();

        builder.Property(csp => csp.FailureReason)
            .HasMaxLength(500)
            .IsRequired(false);

        // Configure relationships
        builder.HasOne(csp => csp.CompanySubscription)
            .WithMany()
            .HasForeignKey(csp => csp.CompanySubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(csp => csp.CompanySubscriptionId)
            .HasDatabaseName("IX_CompanySubscriptionPayment_CompanySubscriptionId");
    }
}

