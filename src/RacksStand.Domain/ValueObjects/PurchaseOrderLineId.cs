using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record PurchaseOrderLineId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public PurchaseOrderLineId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("PurchaseOrderLineId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static PurchaseOrderLineId Of(string value) => new(value);
    public static implicit operator PurchaseOrderLineId(string value) => Of(value);
    public static explicit operator string(PurchaseOrderLineId orderLineId) => orderLineId.Value;
}
