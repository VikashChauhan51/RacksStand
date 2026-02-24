namespace RacksStand.Api.Endpoints.Auth.Mfa.Handlers;

using System.IdentityModel.Tokens.Jwt;
using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Core.Sms;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.Mfa.Models;
using ResultifyCore;

public sealed class SendPhoneCodeHandler : ICommandHandler<SendPhoneCodeRequest, Outcome>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ISmsSender _smsSender;
    private readonly ILogger<SendPhoneCodeHandler> _logger;

    public SendPhoneCodeHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, ISmsSender smsSender, ILogger<SendPhoneCodeHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _smsSender = smsSender;
        _logger = logger;
    }

    public async Task<Outcome> ExecuteAsync(SendPhoneCodeRequest command, CancellationToken ct)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("SendPhoneCode attempted without authenticated user.");
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

        var token = await _userManager.GenerateChangePhoneNumberTokenAsync(appUser, command.PhoneNumber);
        // Send token by SMS (dummy implementation logs it)
        await _smsSender.SendSmsAsync(command.PhoneNumber, $"Your verification code is: {token}");
        _logger.LogInformation("Sent phone verification token to {Phone} for user {UserId}", command.PhoneNumber, userId);
        return Outcome.Success();
    }
}
