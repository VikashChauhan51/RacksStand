using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ModuleId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ModuleId(string value) => _value = value;
    public static ModuleId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModuleId cannot be null or whitespace.");
        }

        return new ModuleId(value);
    }
}
