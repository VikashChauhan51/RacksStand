namespace RacksStand.Api.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities.Core;

public class ApplicationUser : IdentityUser, IEntity<string>, IEntityAudit, IEntityConcurrency
{
    public required string FullName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
