using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record UserId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private UserId(string value) => _value = value;
    public static UserId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserId cannot be null or whitespace.");
        }

        return new UserId(value);
    }
}
