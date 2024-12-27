using Microsoft.AspNetCore.Identity;
using RacksStand.Persistence.ValueObjects;


namespace RacksStand.Persistence.Entities;
public class User : IdentityUser, IEntity
{
    public PlanId? PlanId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Plan Plan { get; set; } = default!;
    public virtual ICollection<Shop> Stores { get; set; } = [];
    public virtual ICollection<UserShop> UserStores { get; set; } = [];
    public virtual ICollection<UserInvite> UserInvites { get; set; } = [];
}
