using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyUserId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private CompanyUserId(string value) => _value = value;
    public static CompanyUserId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserId cannot be null or whitespace.");
        }

        return new CompanyUserId(value);
    }
}

