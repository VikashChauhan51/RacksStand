using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ModulePermissionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    private ModulePermissionId(string value) => _value = value;
    public static ModulePermissionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModulePermissionId cannot be null or whitespace.");
        }

        return new ModulePermissionId(value);
    }
}
