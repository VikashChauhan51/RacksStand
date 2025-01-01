using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record InventoryId
{
    public string Value { get; }
    private InventoryId(string value) => Value = value;
    public static InventoryId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("InventoryId cannot be null or whitespace.");
        }

        return new InventoryId(value);
    }
}
