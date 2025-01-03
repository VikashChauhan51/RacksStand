using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ProductOptionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ProductOptionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductOptionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static ProductOptionId Of(string value) => new(value);
    public static implicit operator ProductOptionId(string value) => Of(value);
    public static explicit operator string(ProductOptionId productOptionId) => productOptionId.Value;
}
