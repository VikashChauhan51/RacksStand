using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private SaleOrderId(string value) => _value = value;
    public static SaleOrderId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        return new SaleOrderId(value);
    }
}
