using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record BinId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public BinId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("BinId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static BinId Of(string value) => new(value);

    public static implicit operator BinId(string value) => Of(value);
    public static explicit operator string(BinId binId) => binId.Value;
}
