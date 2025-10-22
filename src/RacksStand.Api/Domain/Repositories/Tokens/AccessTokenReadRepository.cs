namespace RacksStand.Api.Domain.Repositories.Tokens;

using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;

public sealed class AccessTokenReadRepository : IAccessTokenReadRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly ILogger<AccessTokenReadRepository> _logger;

    public AccessTokenReadRepository(
        ILogger<AccessTokenReadRepository> logger,
        IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    public async Task<AccessToken?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting AccessToken by Id: {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.AccessTokens.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<AccessToken?> GetByIdWithUserAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting AccessToken with User by Id: {Id}", id);
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.AccessTokens.AsNoTracking()
            .AsSplitQuery()
            .Include(at => at.User)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
