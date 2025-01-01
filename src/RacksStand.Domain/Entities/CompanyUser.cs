using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class CompanyUser : Entity<CompanyUserId>
{
    public required UserId UserId { get; set; }
    public required CompanyId CompanyId { get; set; }

}
