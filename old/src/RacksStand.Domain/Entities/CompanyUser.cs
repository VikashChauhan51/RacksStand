using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class CompanyUser : Entity<CompanyUserId>
{
    public required UserId UserId { get; set; }
    public required CompanyId CompanyId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Company Company { get; set;} = null!;

}
