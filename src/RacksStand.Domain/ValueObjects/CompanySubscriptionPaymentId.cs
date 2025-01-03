using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanySubscriptionPaymentId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanySubscriptionPaymentId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionPaymentId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static CompanySubscriptionPaymentId Of(string value) => new(value);

    public static implicit operator CompanySubscriptionPaymentId(string value) => Of(value);
    public static explicit operator string(CompanySubscriptionPaymentId subscriptionPaymentId) => subscriptionPaymentId.Value;
}
