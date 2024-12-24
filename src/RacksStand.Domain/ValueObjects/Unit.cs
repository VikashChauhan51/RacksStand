using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record Unit
{
    public string Value { get; }

    private Unit(string value) => Value = value;

    public static Unit Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Unit cannot be null or whitespace.");
        }

        return new Unit(value);
    }
}
