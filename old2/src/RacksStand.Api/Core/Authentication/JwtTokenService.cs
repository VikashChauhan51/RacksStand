namespace RacksStand.Api.Core.Authentication;

using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public class JwtTokenService : IJwtTokenService
{
    private readonly IAccessTokenWriteRepository _accessTokenWriteRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly ILogger<JwtTokenService> _logger;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    public JwtTokenService(
        ILogger<JwtTokenService> logger,
        IAccessTokenWriteRepository accessTokenWriteRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IJwtTokenProvider jwtTokenProvider)
    {
        _logger = logger;
        _accessTokenWriteRepository = accessTokenWriteRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _jwtTokenProvider = jwtTokenProvider;
    }
    public async Task<string?> GenerateAccessTokenAsync(ApplicationUser user, IEnumerable<string> roles, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        _logger.LogInformation("Generating access token for user {UserId}", user.Id);
        var now = DateTimeOffset.UtcNow;
        var tokenResponse = _jwtTokenProvider.GenerateAccessToken(user.Id, user.Email!, roles);
        AccessToken accessToken = new AccessToken
        {
            Id = tokenResponse.Jti,
            UserId = user.Id,
            ExpiresAt = tokenResponse.ExpiresAt
        };
        var response = await _accessTokenWriteRepository.AddAsync(accessToken, cancellationToken);
        if (response == null)
        {
            _logger.LogError("Failed to save access token for user {UserId}", user.Id);
            return null;
        }
        return tokenResponse.Token;
    }

    public async Task<string?> GenerateRefreshTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        _logger.LogInformation("Generating refresh token for user {UserId}", user.Id);
        var now = DateTimeOffset.UtcNow;
        var tokenResponse = _jwtTokenProvider.GenerateRefreshToken(user.Id);
        RefreshToken refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            ExpiresAt = tokenResponse.ExpiresAt,
            TokenHash = tokenResponse.HashedToken
        };

        var response = await _refreshTokenWriteRepository.AddAsync(refreshToken, cancellationToken);

        if (response == null)
        {
            _logger.LogError("Failed to save refresh token for user {UserId}", user.Id);
            return null;
        }
        return tokenResponse.PlainToken;
    }
}
