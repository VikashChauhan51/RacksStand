using RacksStand.Domain.Exceptions;

namespace RacksStand.Domain.ValueObjects;

public record ModulePermissionId
{
    public string Value { get; }
    private ModulePermissionId(string value) => Value = value;
    public static ModulePermissionId Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("ModulePermissionId cannot be null or whitespace.");
        }

        return new ModulePermissionId(value);
    }
}
