using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record CompanyId
{
    public string Value { get; }
    private CompanyId(string value) => Value = value;
    public static CompanyId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CompanyId cannot be null or whitespace.");
        }

        return new CompanyId(value);
    }
}

