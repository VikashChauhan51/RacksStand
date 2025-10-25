namespace RacksStand.Api.Endpoints.Auth.GetProfile.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.GetProfile.Models;
using ResultifyCore;

public sealed class GetProfileHandler : ICommandHandler<GetProfileRequest, Outcome<GetProfileResponse?>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<GetProfileHandler> _logger;

    public GetProfileHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ILogger<GetProfileHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome<GetProfileResponse?>> ExecuteAsync(GetProfileRequest command, CancellationToken ct)
    {
        var userPrincipal = _httpContextAccessor.HttpContext?.User;
        if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("GetProfile attempted without authenticated user.");
            return Outcome<GetProfileResponse?>.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = userPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            _logger.LogWarning("GetProfile attempted but no subject claim found.");
            return Outcome<GetProfileResponse?>.Problem(new OutcomeError("InvalidToken", "Access token does not contain subject claim."));
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null || user.DeletedAt != null)
        {
            _logger.LogWarning("User not found for profile request (id: {UserId}).", userId);
            return Outcome<GetProfileResponse?>.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        var roles = await _userManager.GetRolesAsync(user);
        var response = new GetProfileResponse(user.Id, user.Email ?? string.Empty, user.FullName, user.EmailConfirmed, roles ?? []);
        return Outcome<GetProfileResponse?>.Success(response);
    }
}
