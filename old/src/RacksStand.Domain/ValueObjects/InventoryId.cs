using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record InventoryId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public InventoryId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("InventoryId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static InventoryId Of(string value) => new(value);
    public static implicit operator InventoryId(string value) => Of(value);
    public static explicit operator string(InventoryId inventoryId) => inventoryId.Value;
}
