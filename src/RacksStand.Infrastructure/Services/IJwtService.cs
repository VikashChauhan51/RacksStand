using RacksStand.Domain.Entities;

namespace RacksStand.Api.Core.Infrastructure.Services;
public interface IJwtService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}