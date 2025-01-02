using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanySubscriptionPaymentId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private CompanySubscriptionPaymentId(string value) => _value = value;
    public static CompanySubscriptionPaymentId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionPaymentId cannot be null or whitespace.");
        }

        return new CompanySubscriptionPaymentId(value);
    }
}
