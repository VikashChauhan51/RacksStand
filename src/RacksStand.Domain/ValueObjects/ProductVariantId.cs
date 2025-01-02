using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ProductVariantId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ProductVariantId(string value) => _value = value;
    public static ProductVariantId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductVariantId cannot be null or whitespace.");
        }

        return new ProductVariantId(value);
    }
}
