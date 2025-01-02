using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record RackId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private RackId(string value) => _value = value;
    public static RackId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("RackId cannot be null or whitespace.");
        }
        return new RackId(value);
    }
}
