using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ProductVariantId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ProductVariantId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static ProductVariantId Of(string value) => new(value);
    public static implicit operator ProductVariantId(string value) => Of(value);
    public static explicit operator string(ProductVariantId productVariantId) => productVariantId.Value;
}
