using RacksStand.Api.Core.Authentication.Models;

namespace RacksStand.Api.Core.Authentication.Abstractions;

public interface IJwtTokenProvider
{
    AccessTokenResult GenerateAccessToken(string userId, string email, IEnumerable<string> roles);
    RefreshTokenResult GenerateRefreshToken(string userId);
    string ComputeRefreshTokenHash(string plainToken);
}
