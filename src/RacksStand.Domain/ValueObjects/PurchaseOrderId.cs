using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PurchaseOrderId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private PurchaseOrderId(string value) => _value = value;
    public static PurchaseOrderId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderId cannot be null or whitespace.");
        }

        return new PurchaseOrderId(value);
    }
}
