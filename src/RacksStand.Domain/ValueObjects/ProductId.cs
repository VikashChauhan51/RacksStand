using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ProductId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ProductId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static ProductId Of(string value) => new(value);
    public static implicit operator ProductId(string value) => Of(value);
    public static explicit operator string(ProductId productId) => productId.Value;
}
