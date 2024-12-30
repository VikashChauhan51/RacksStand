using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Features.UserShops.Domain.Entities;

namespace RacksStand.Api.Features.Users.Domain.Entities;
public class User : IdentityUser, IEntity<string>
{
    public required string FullName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual ICollection<UserShop> UserStores { get; set; } = [];
    public virtual ICollection<UserInvite> UserInvites { get; set; } = [];
    public virtual ICollection<UserPlan> UserPlans { get; set; } = [];
}
