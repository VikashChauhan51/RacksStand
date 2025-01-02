using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductOptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ProductOptionId(string value) => _value = value;
    public static ProductOptionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductOptionId cannot be null or whitespace.");
        }

        return new ProductOptionId(value);
    }
}
