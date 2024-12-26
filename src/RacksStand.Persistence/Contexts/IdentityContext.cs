using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace RacksStand.Persistence.Contexts;
public interface IdentityContext
{
    DbSet<IdentityUser> Users { get; }
    DbSet<IdentityUserLogin<string>> UserLogins { get; }
    DbSet<IdentityUserClaim<string>> UserClaims { get; }
    DbSet<IdentityUserToken<string>> UserTokens { get; }
    DbSet<IdentityUserRole<string>> UserRoles { get; }
    DbSet<IdentityRole> Roles { get; }
    DbSet<IdentityRoleClaim<string>> RoleClaims { get; }
}
