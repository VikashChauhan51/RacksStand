namespace RacksStand.Api.Domain.Entities;

public class AccessToken: IEntity<string>, IEntityAudit 
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public ApplicationUser? User { get; set; }

}
