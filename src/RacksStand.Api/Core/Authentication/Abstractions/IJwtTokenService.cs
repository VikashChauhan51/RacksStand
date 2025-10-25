using RacksStand.Api.Domain.Entities;

namespace RacksStand.Api.Core.Authentication.Abstractions;

public interface IJwtTokenService
{
    Task<string?> GenerateAccessTokenAsync(ApplicationUser user, IEnumerable<string> roles, CancellationToken cancellationToken=default);
    Task<string?> GenerateRefreshTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}
