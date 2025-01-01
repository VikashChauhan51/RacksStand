using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanySubscriptionId
{
    public string Value { get; }
    private CompanySubscriptionId(string value) => Value = value;
    public static CompanySubscriptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionId cannot be null or whitespace.");
        }

        return new CompanySubscriptionId(value);
    }
}
