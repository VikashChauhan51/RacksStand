using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public CompanyId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static CompanyId Of(string value) => new(value);

    public static implicit operator CompanyId(string value) => Of(value);
    public static explicit operator string(CompanyId companyId) => companyId.Value;
}

