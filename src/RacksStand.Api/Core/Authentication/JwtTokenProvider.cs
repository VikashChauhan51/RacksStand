namespace RacksStand.Api.Core.Authentication;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Core.Authentication.Models;
using RacksStand.Api.Core.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly ILogger<JwtTokenProvider> _logger;
    private readonly AuthenticationConfiguration _authenticationConfiguration;
    public JwtTokenProvider(
        ILogger<JwtTokenProvider> logger,
        IOptions<AuthenticationConfiguration> options
        )
    {
        _logger = logger;
        _authenticationConfiguration = options?.Value ?? throw new ValidationException("Authentication configuration is missing.");

    }
    public AccessTokenResult GenerateAccessToken(string userId, string email, IEnumerable<string> roles)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(email);
        _logger.LogInformation("Generating access token for user {UserId}", userId);

        var now = DateTimeOffset.UtcNow;
        var jit = Guid.NewGuid().ToString();
        var expires = now.AddMinutes(_authenticationConfiguration.AccessTokenExpirationMinutes);
        var secretBytes = Encoding.UTF8.GetBytes(_authenticationConfiguration.JwtSecret);
        var signingKey = new SymmetricSecurityKey(secretBytes);
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, jit),
        };

        foreach (var role in roles ?? [])
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _authenticationConfiguration.Issuer,
            audience: _authenticationConfiguration.Audience,
            claims: claims,
            notBefore: now.UtcDateTime,
            expires: expires.UtcDateTime,
            signingCredentials: credentials
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.WriteToken(tokenDescriptor);
        _logger.LogInformation("Access token generated for user {UserId}", userId);
        return new AccessTokenResult(token, jit, expires);
    }

    public RefreshTokenResult GenerateRefreshToken(string userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        var now = DateTimeOffset.UtcNow;
        var expires = now.AddDays(_authenticationConfiguration.RefreshTokenExpirationDays);
        var randomBytes = new byte[64]; // 512 bits of entropy
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var baseToken = Convert.ToBase64String(randomBytes);
        var encodedUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(userId));
        var plainToken = $"{encodedUserId}.{now.ToUnixTimeMilliseconds()}.{baseToken}";

        using var sha512 = SHA512.Create();
        var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainToken));
        var hashedToken = Convert.ToBase64String(hashBytes); ;

        _logger.LogInformation("Generating refresh token for user {UserId}", userId);
        return  new RefreshTokenResult(plainToken, hashedToken, expires);

    }

    public string ComputeRefreshTokenHash(string plainToken)
    {
        ArgumentNullException.ThrowIfNull(plainToken);
        _logger.LogInformation("Computing refresh token hash.");
        using var sha512 = SHA512.Create();
        var hashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainToken));
        return Convert.ToBase64String(hashBytes);
    }
}
