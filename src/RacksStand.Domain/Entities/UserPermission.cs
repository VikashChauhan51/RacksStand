using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class UserPermission : Entity<UserPermissionId>
{
    public required ModulePermissionId ModulePermissionId { get; set; }
    public required UserId UserId { get; set; }

    public virtual ModulePermission ModulePermission { get; private set; } = null!;
    public virtual User User { get; private set; } = null!;
}
