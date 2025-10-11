using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class CompanyUserInvite : Entity<CompanyUserInviteId>
{
    public required CompanyId CompanyId { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public DateTimeOffset ExpiryDate { get; set; }
    public InviteState Status { get; set; }

    // Navigation properties
    public Company Company { get; set; } = null!;
}
