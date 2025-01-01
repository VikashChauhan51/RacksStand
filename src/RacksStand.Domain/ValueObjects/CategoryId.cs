using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record CategoryId
{
    public string Value { get; }
    private CategoryId(string value) => Value = value;
    public static CategoryId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("CategoryId cannot be null or whitespace.");
        }

        return new CategoryId(value);
    }
}
