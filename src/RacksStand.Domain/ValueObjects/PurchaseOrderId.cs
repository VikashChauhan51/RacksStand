using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PurchaseOrderId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public PurchaseOrderId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderId cannot be null or whitespace.");
        }

        _value = value;
    }
    public static PurchaseOrderId Of(string value) => new(value);
    public static implicit operator PurchaseOrderId(string value) => Of(value);
    public static explicit operator string(PurchaseOrderId purchaseOrderId) => purchaseOrderId.Value;
}
