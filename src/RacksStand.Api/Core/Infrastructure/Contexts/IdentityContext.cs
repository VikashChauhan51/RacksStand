using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Core.Infrastructure.Contexts;
public interface IdentityContext
{
    DbSet<User> Users { get; }
    DbSet<IdentityUserLogin<string>> UserLogins { get; }
    DbSet<IdentityUserClaim<string>> UserClaims { get; }
    DbSet<IdentityUserToken<string>> UserTokens { get; }
    DbSet<IdentityUserRole<string>> UserRoles { get; }
    DbSet<IdentityRole> Roles { get; }
    DbSet<IdentityRoleClaim<string>> RoleClaims { get; }
}
