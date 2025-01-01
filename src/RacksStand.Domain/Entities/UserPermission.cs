using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class UserPermission : Entity<UserPermissionId>
{
    public required ModulePermissionId ModulePermissionId { get; set; }
    public required UserId UserId { get; set; }
}
