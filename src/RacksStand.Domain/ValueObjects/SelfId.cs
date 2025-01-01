using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record SelfId
{
    public string Value { get; }
    private SelfId(string value) => Value = value;
    public static SelfId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SelfId cannot be null or whitespace.");
        }

        return new SelfId(value);
    }
}
