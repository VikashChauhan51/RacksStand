namespace RacksStand.Api.Endpoints.Auth.Sessions.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.Sessions.Models;
using ResultifyCore;

public sealed class SessionsHandler : ICommandHandler<SessionsRequest, Outcome<IEnumerable<SessionDto>>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly ILogger<SessionsHandler> _logger;

    public SessionsHandler(IHttpContextAccessor httpContextAccessor, IRefreshTokenReadRepository refreshTokenReadRepository, ILogger<SessionsHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _logger = logger;
    }

    public async Task<Outcome<IEnumerable<SessionDto>>> ExecuteAsync(SessionsRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Sessions list attempted without authenticated user.");
            return Outcome<IEnumerable<SessionDto>>.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome<IEnumerable<SessionDto>>.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        var tokens = await _refreshTokenReadRepository.GetByUserIdAsync(userId, ct);
        var dtos = tokens.Select(t => new SessionDto(t.Id, t.CreatedAt, t.ExpiresAt, t.DeletedAt));
        return Outcome<IEnumerable<SessionDto>>.Success(dtos);
    }
}
