using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Core.Configurations;
using RacksStand.Api.Core.Resilience;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Domain.Repositories.Abstractions.Tokens;
using RacksStand.Api.Endpoints.Auth.RefreshToken.Models;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.RefreshToken.Handlers;

public sealed class RefreshTokenHandler : ICommandHandler<RefreshTokenRequest, Outcome<RefreshTokenResponse?>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<RefreshTokenHandler> _logger;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPolicyService _policyService;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly IRefreshTokenReadRepository _refreshTokenReadRepository;
    private readonly IRefreshTokenWriteRepository _refreshTokenWriteRepository;
    private readonly AuthenticationConfiguration _authenticationConfiguration;

    public RefreshTokenHandler(
        ILogger<RefreshTokenHandler> logger,
        UserManager<ApplicationUser> userManager,
        IJwtTokenService jwtTokenService,
        IJwtTokenProvider jwtTokenProvider,
        IPolicyService policyService,
        IRefreshTokenReadRepository refreshTokenReadRepository,
        IRefreshTokenWriteRepository refreshTokenWriteRepository,
        IOptions<AuthenticationConfiguration> options)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _jwtTokenProvider = jwtTokenProvider;
        _policyService = policyService;
        _refreshTokenReadRepository = refreshTokenReadRepository;
        _refreshTokenWriteRepository = refreshTokenWriteRepository;
        _authenticationConfiguration = options?.Value ?? throw new ValidationException("Authentication configuration missing.");
    }

    public async Task<Outcome<RefreshTokenResponse?>> ExecuteAsync(RefreshTokenRequest request, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
        {
            return Outcome<RefreshTokenResponse?>.Problem(new OutcomeError("InvalidRequest", "Refresh token is required."));
        }

        var refreshTokenHash = _jwtTokenProvider.ComputeRefreshTokenHash(request.RefreshToken);
        var storedRefreshToken = await _refreshTokenReadRepository.GetByTokenHashAsync(refreshTokenHash, ct);
        if (storedRefreshToken == null ||
            storedRefreshToken.ExpiresAt <= DateTimeOffset.UtcNow)
        {
            _logger.LogWarning("Invalid or expired refresh token.");
            return Outcome<RefreshTokenResponse?>.Unauthorized(new OutcomeError("InvalidToken", "Refresh token is invalid or expired."));
        }

        var user = await _userManager.FindByIdAsync(storedRefreshToken.UserId);
        if (user == null || user.DeletedAt != null)
        {
            _logger.LogWarning("User not found or deleted for refresh token.");
            return Outcome<RefreshTokenResponse?>.NotFound(new OutcomeError("UserNotFound", "User does not exist."));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var retryPolicy = _policyService.CreateCustomPolicy(
            _authenticationConfiguration.MaxRetryAttempts,
            _authenticationConfiguration.DefaultRetryDelayMilliseconds);

        await _refreshTokenWriteRepository.RevokedAsync(storedRefreshToken.Id, ct);

        // Generate new access token
        var newAccessToken = await retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogDebug("Generating new access token for user {UserId}", user.Id);
            var token = await _jwtTokenService.GenerateAccessTokenAsync(user, roles ?? [], ct);
            if (token == null)
            {
                throw new InvalidOperationException("Failed to generate access token.");
            }
            return token;
        });

        // Optionally issue a new refresh token
        var newRefreshToken = await retryPolicy.ExecuteAsync(async () =>
        {
            var token = await _jwtTokenService.GenerateRefreshTokenAsync(user, ct);
            if (token == null)
            {
                throw new InvalidOperationException("Failed to generate refresh token.");
            }
            return token;
        });

        return Outcome<RefreshTokenResponse?>.Success(new RefreshTokenResponse(newAccessToken, newRefreshToken));
    }
}
