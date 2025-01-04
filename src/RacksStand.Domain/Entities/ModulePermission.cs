using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;


namespace RacksStand.Domain.Entities;
public class ModulePermission : Entity<ModulePermissionId>
{
    public required ModuleId ModuleId { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public required string Description { get; set; }
    public virtual Module Module { get; set; } = null!;
}
