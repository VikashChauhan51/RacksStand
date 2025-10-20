namespace RacksStand.Api.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities.Core;

public class ApplicationRole : IdentityRole, IEntity<string>, IEntityAudit, IEntityConcurrency
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
