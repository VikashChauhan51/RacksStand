using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CustomerId
{
    public string Value { get; }
    private CustomerId(string value) => Value = value;
    public static CustomerId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CustomerId cannot be null or whitespace.");
        }

        return new CustomerId(value);
    }
}
