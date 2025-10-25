namespace RacksStand.Api.Domain.Repositories.Tokens;

using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public sealed class AccessTokenWriteRepository : IAccessTokenWriteRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AccessTokenWriteRepository> _logger;
    public AccessTokenWriteRepository(
        ILogger<AccessTokenWriteRepository> logger,
        ApplicationDbContext context
        )
    {
        _logger = logger;
        _context = context;

    }
    public async Task<AccessToken?> AddAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(accessToken, nameof(accessToken));
        _logger.LogInformation("Adding access token for user {UserId}", accessToken.UserId);
        _context.AccessTokens.Add(accessToken);
        await _context.SaveChangesAsync();
        return accessToken;
    }
}
