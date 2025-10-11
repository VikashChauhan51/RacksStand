using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CompanySubscriptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanySubscriptionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanySubscriptionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static CompanySubscriptionId Of(string value) => new(value);
    public static implicit operator CompanySubscriptionId(string value) => Of(value);
    public static explicit operator string(CompanySubscriptionId companySubscriptionId) => companySubscriptionId.Value;
}
