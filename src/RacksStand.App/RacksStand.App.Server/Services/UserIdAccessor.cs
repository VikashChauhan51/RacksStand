// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2024 www.ebenmonney.com/mit-license
// ---------------------------------------

using RacksStand.App.Core.Services.Account;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace RacksStand.App.Server.Services;
public class UserIdAccessor(IHttpContextAccessor httpContextAccessor) : IUserIdAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? GetCurrentUserId() => _httpContextAccessor.HttpContext?.User.FindFirstValue(Claims.Subject);
}

public class SystemUserIdAccessor : IUserIdAccessor
{
    private readonly string? id;

    private SystemUserIdAccessor(string? id) => this.id = id;

    public string? GetCurrentUserId() => id;

    public static SystemUserIdAccessor GetNewAccessor(string? id = "SYSTEM") => new(id);
}
