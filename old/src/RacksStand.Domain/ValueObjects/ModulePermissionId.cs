using RacksStand.Domain.Core;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ModulePermissionId : IValueObject<string>
{
    private readonly string _value;
    public string Value => _value;
    public ModulePermissionId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModulePermissionId cannot be null or whitespace.");
        }
        _value = value;
    }
    public static ModulePermissionId Of(string value) => new(value);
    public static implicit operator ModulePermissionId(string value) => Of(value);
    public static explicit operator string(ModulePermissionId modulePermissionId) => modulePermissionId.Value;
}
