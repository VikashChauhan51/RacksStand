namespace RacksStand.Api.Core.Authentication;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Core.Configurations;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AccessTokenValidator : IAccessTokenValidator
{
    private readonly IAccessTokenReadRepository _accessTokenReadRepository;
    private readonly AuthenticationConfiguration _authConfig;
    private readonly ILogger<AccessTokenValidator> _logger;

    public AccessTokenValidator(
        IAccessTokenReadRepository accessTokenReadRepository,
        IOptions<AuthenticationConfiguration> options,
        ILogger<AccessTokenValidator> logger)
    {
        _accessTokenReadRepository = accessTokenReadRepository;
        _authConfig = options?.Value ?? throw new ValidationException("Authentication configuration is missing.");
        _logger = logger;
    }

    public async Task<ClaimsPrincipal?> ValidateAsync(string token, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_authConfig.JwtSecret);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _authConfig.Issuer,
                ValidAudience = _authConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jti = principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var userId = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(jti) || string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("JWT missing JTI or Sub claim");
                return null;
            }

            var storedToken = await _accessTokenReadRepository.GetByIdAsync(jti, cancellationToken);
            if (storedToken == null || storedToken.UserId != userId)
            {
                _logger.LogWarning("Access token is revoked or does not exist in DB");
                return null;
            }

            return principal;
        }
        catch (SecurityTokenExpiredException)
        {
            _logger.LogWarning("JWT token expired");
            return null;
        }
        catch (SecurityTokenException ex)
        {
            _logger.LogWarning(ex, "JWT token validation failed");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during JWT validation");
            return null;
        }
    }
}

