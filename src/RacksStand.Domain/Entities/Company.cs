using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Company : Entity<CompanyId>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
    public required UserId OwnerUserId { get; set; }
    public required PlanId PlanId { get; set; }
}
