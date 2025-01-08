using System.Security.Claims;

namespace RacksStand.Api.Extensions;

public static class ClaimsPrincipalExtensions
{

    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? GetUserName(this HttpContext httpContext)
    {
        return httpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
    public static string? GetPermission(this ClaimsPrincipal user, string name)
    {
        var claim = user?.FindFirst(c => c.Type == "scope" && c.Value == name);
        return claim?.Value;
    }
    public static string? GetPermission(this HttpContext httpContext, string name)
    {
        var claim = httpContext?.User?.FindFirst(c => c.Type == "scope" && c.Value == name);
        return claim?.Value;
    }

}
