namespace RacksStand.Api.Endpoints.Auth.Mfa.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public sealed class AuthenticatorSetupHandler : ICommandHandler<AuthenticatorSetupRequest, Outcome<AuthenticatorSetupResponse?>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AuthenticatorSetupHandler> _logger;

    public AuthenticatorSetupHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ILogger<AuthenticatorSetupHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome<AuthenticatorSetupResponse?>> ExecuteAsync(AuthenticatorSetupRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("Authenticator setup attempted without authenticated user.");
            return Outcome<AuthenticatorSetupResponse?>.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome<AuthenticatorSetupResponse?>.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
        {
            return Outcome<AuthenticatorSetupResponse?>.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        // Ensure key exists
        var key = await _userManager.GetAuthenticatorKeyAsync(appUser);
        if (string.IsNullOrEmpty(key))
        {
            await _userManager.ResetAuthenticatorKeyAsync(appUser);
            key = await _userManager.GetAuthenticatorKeyAsync(appUser);
        }

        // The manual entry key is usually the same key formatted in groups for display
        var manual = key; // formatting can be applied by client

        return Outcome<AuthenticatorSetupResponse?>.Success(new AuthenticatorSetupResponse(key, manual));
    }
}
