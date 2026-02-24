using System.Security.Claims;

namespace RacksStand.Api.Core.Authentication.Abstractions;

public interface IAccessTokenValidator
{
    Task<ClaimsPrincipal?> ValidateAsync(string token, CancellationToken cancellationToken = default);

}
