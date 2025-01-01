using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductVariantOptionId
{
    public string Value { get; }
    private ProductVariantOptionId(string value) => Value = value;
    public static ProductVariantOptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantOptionId cannot be null or whitespace.");
        }

        return new ProductVariantOptionId(value);
    }
}
