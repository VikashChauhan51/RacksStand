using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record WarehouseId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private WarehouseId(string value) => _value = value;
    public static WarehouseId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("WarehouseId cannot be null or whitespace.");
        }

        return new WarehouseId(value);
    }
}
