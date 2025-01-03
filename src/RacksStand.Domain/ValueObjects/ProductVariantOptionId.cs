using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductVariantOptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ProductVariantOptionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantOptionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static ProductVariantOptionId Of(string value) => new(value);
    public static implicit operator ProductVariantOptionId(string value) => Of(value);
    public static explicit operator string(ProductVariantOptionId variantOptionId) => variantOptionId.Value;
}
