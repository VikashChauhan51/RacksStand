using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductVariantOptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ProductVariantOptionId(string value) => _value = value;
    public static ProductVariantOptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantOptionId cannot be null or whitespace.");
        }

        return new ProductVariantOptionId(value);
    }
}
