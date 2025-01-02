using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record SaleOrderLineId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private SaleOrderLineId(string value) => _value = value;
    public static SaleOrderLineId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SaleOrderId cannot be null or whitespace.");
        }

        return new SaleOrderLineId(value);
    }
}
