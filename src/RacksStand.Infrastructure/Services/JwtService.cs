using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RacksStand.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace RacksStand.Api.Core.Infrastructure.Services;
public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.Value),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        claims.AddRange(user.UserPermissions.Select(x => new Claim("scope", x.ModulePermission.Code)).ToList());
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var ticksBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
        var guidBytes = Guid.NewGuid().ToByteArray();

        // Concatenate all byte arrays
        var allBytes = new byte[randomBytes.Length + ticksBytes.Length + guidBytes.Length];
        Buffer.BlockCopy(randomBytes, 0, allBytes, 0, randomBytes.Length);
        Buffer.BlockCopy(ticksBytes, 0, allBytes, randomBytes.Length, ticksBytes.Length);
        Buffer.BlockCopy(guidBytes, 0, allBytes, randomBytes.Length + ticksBytes.Length, guidBytes.Length);

        // Convert concatenated byte array to Base64 string
        var base64String = Convert.ToBase64String(allBytes);
        return base64String;
    }

}

