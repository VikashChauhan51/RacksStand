using RacksStand.Persistence.Exceptions;

namespace RacksStand.Persistence.ValueObjects;

public record UserId
{
    public string Value { get; }
    private UserId(string value) => Value = value;
    public static UserId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("UserId cannot be null or whitespace.");
        }

        return new UserId(value);
    }
}
