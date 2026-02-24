namespace RacksStand.Api.Endpoints.Auth.Mfa.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public sealed class VerifyPhoneCodeHandler : ICommandHandler<VerifyPhoneCodeRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<VerifyPhoneCodeHandler> _logger;

    public VerifyPhoneCodeHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ILogger<VerifyPhoneCodeHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(VerifyPhoneCodeRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("VerifyPhoneCode attempted without authenticated user.");
            return Outcome.Unauthorized(new OutcomeError("Unauthorized", "User is not authenticated."));
        }

        var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Outcome.Problem(new OutcomeError("InvalidToken", "Access token missing subject."));
        }

        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
        {
            return Outcome.NotFound(new OutcomeError("UserNotFound", "User not found."));
        }

        var result = await _userManager.ChangePhoneNumberAsync(appUser, command.PhoneNumber, command.Code);
        if (!result.Succeeded)
        {
            return Outcome.Validation(result.Errors.Select(e => new OutcomeError(e.Code, e.Description)));
        }

        _logger.LogInformation("Phone number {Phone} verified for user {UserId}", command.PhoneNumber, userId);
        return Outcome.Success();
    }
}
