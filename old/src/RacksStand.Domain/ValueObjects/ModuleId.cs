using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ModuleId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ModuleId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModuleId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static ModuleId Of(string value) => new(value);
    public static implicit operator ModuleId(string value) => Of(value);
    public static explicit operator string(ModuleId moduleId) => moduleId.Value;
}
