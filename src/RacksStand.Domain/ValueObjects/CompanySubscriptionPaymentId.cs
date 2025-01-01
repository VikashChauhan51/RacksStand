using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanySubscriptionPaymentId
{
    public string Value { get; }
    private CompanySubscriptionPaymentId(string value) => Value = value;
    public static CompanySubscriptionPaymentId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionPaymentId cannot be null or whitespace.");
        }

        return new CompanySubscriptionPaymentId(value);
    }
}
