using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record WarehouseId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public WarehouseId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("WarehouseId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static WarehouseId Of(string value) => new(value);
    public static implicit operator WarehouseId(string value) => Of(value);
    public static explicit operator string(WarehouseId warehouseId) => warehouseId.Value;
}
