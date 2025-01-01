using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ProductVariantId
{
    public string Value { get; }
    private ProductVariantId(string value) => Value = value;
    public static ProductVariantId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantId cannot be null or whitespace.");
        }

        return new ProductVariantId(value);
    }
}
