using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductOptionId
{
    public string Value { get; }
    private ProductOptionId(string value) => Value = value;
    public static ProductOptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductOptionId cannot be null or whitespace.");
        }

        return new ProductOptionId(value);
    }
}
