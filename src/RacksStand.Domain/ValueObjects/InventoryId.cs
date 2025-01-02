using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record InventoryId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private InventoryId(string value) => _value = value;
    public static InventoryId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("InventoryId cannot be null or whitespace.");
        }

        return new InventoryId(value);
    }
}
