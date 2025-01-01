using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class Module : Entity<ModuleId>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public virtual ICollection<ModulePermission> Permissions { get; set; } = [];
}
