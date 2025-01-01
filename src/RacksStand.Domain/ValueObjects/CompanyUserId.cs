using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyUserId
{
    public string Value { get; }
    private CompanyUserId(string value) => Value = value;
    public static CompanyUserId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyUserId cannot be null or whitespace.");
        }

        return new CompanyUserId(value);
    }
}

