using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ProductId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ProductId(string value) => _value = value;
    public static ProductId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ProductId cannot be null or whitespace.");
        }

        return new ProductId(value);
    }
}
