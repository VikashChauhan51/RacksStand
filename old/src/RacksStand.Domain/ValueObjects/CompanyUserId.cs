using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyUserId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanyUserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static CompanyUserId Of(string value) => new(value);

    public static implicit operator CompanyUserId(string value) => Of(value);
    public static explicit operator string(CompanyUserId companyUserId) => companyUserId.Value;
}

