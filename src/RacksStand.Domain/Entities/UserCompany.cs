using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class UserCompany : Entity<UserCompanyId>
{
    public required UserId UserId { get; set; }
    public required CompanyId CompanyId { get; set; }

}
