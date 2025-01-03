using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderLineId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public SaleOrderLineId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static SaleOrderLineId Of(string value) => new(value);
    public static implicit operator SaleOrderLineId(string value) => Of(value);
    public static explicit operator string(SaleOrderLineId orderLineId) => orderLineId.Value;
}
