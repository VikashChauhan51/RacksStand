using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public SaleOrderId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static SaleOrderId Of(string value) => new(value);
    public static implicit operator SaleOrderId(string value) => Of(value);
    public static explicit operator string(SaleOrderId saleOrderId) => saleOrderId.Value;
}
