using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanySubscriptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanySubscriptionId(string value)
    {
        _value = value;
    }
    public static CompanySubscriptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionId cannot be null or whitespace.");
        }

        return new CompanySubscriptionId(value);
    }
}
