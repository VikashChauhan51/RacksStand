using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserCompanyId
{
    public string Value { get; }
    private UserCompanyId(string value) => Value = value;
    public static UserCompanyId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserCompanyId cannot be null or whitespace.");
        }

        return new UserCompanyId(value);
    }
}

