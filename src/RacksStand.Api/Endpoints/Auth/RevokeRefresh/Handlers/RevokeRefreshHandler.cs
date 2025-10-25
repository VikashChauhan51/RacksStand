namespace RacksStand.Api.Endpoints.Auth.RevokeRefresh.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.RevokeRefresh.Models;
using ResultifyCore;

public sealed class RevokeRefreshHandler : ICommandHandler<RevokeRefreshRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly ILogger<RevokeRefreshHandler> _logger;

    public RevokeRefreshHandler(
        IHttpContextAccessor httpContextAccessor,
        IJwtTokenProvider jwtTokenProvider,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        ILogger<RevokeRefreshHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtTokenProvider = jwtTokenProvider;
        _refreshTokenReadRepository = refreshTokenReadRepository;
    _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(RevokeRefreshRequest command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.RefreshToken))
        {
            return Outcome.Problem(new OutcomeError("InvalidRequest", "Refresh token is required."));
        }

        var userPrincipal = _httpContextAccessor.HttpContext?.User;
        if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Revoke refresh attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = userPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome.Problem(new OutcomeError("InvalidToken", "No subject claim found."));
        }

        var hash = _jwtTokenProvider.ComputeRefreshTokenHash(command.RefreshToken);
        var stored = await _refreshTokenReadRepository.GetByTokenHashAsync(hash, ct);
        if (stored == null)
        {
            _logger.LogWarning("Refresh token not found for revocation (user {UserId}).", userId);
            return Outcome.NotFound(new OutcomeError("TokenNotFound", "Refresh token not found."));
        }

        if (stored.UserId != userId)
        {
            _logger.LogWarning("User {UserId} attempted to revoke refresh token belonging to {OwnerId}.", userId, stored.UserId);
            return Outcome.Forbidden(new OutcomeError("Forbidden", "Cannot revoke token for another user."));
        }

        await _refreshTokenWriteRepository.RevokedAsync(stored.Id, ct);
        _logger.LogInformation("Refresh token {RefreshId} revoked for user {UserId}.", stored.Id, userId);
        return Outcome.Success();
    }
}
