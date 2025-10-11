using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record RackId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public RackId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("RackId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static RackId Of(string value) => new(value);
    public static implicit operator RackId(string value) => Of(value);
    public static explicit operator string(RackId rackId) => rackId.Value;
}
