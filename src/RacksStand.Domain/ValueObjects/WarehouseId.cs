using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record WarehouseId
{
    public string Value { get; }
    private WarehouseId(string value) => Value = value;
    public static WarehouseId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("WarehouseId cannot be null or whitespace.");
        }

        return new WarehouseId(value);
    }
}
