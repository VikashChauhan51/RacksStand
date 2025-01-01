using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;
public record ModuleId
{
    public string Value { get; }
    private ModuleId(string value) => Value = value;
    public static ModuleId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModuleId cannot be null or whitespace.");
        }

        return new ModuleId(value);
    }
}
