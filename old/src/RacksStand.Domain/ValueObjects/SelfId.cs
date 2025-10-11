using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record SelfId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public SelfId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("SelfId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static SelfId Of(string value) => new(value);
    public static implicit operator SelfId(string value) => Of(value);
    public static explicit operator string(SelfId selfId) => selfId.Value;
}
