using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.ConfirmAccount.Models;
using ResultifyCore;
using System.Text;

namespace RacksStand.Api.Endpoints.Auth.ConfirmAccount.Handlers;

public sealed class ConfirmEmailHandler : ICommandHandler<ConfirmEmailRequest, Outcome>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<ConfirmEmailHandler> _logger;
    private readonly IValidator<ConfirmEmailRequest> _validator;

    public ConfirmEmailHandler(
        UserManager<ApplicationUser> userManager,
        ILogger<ConfirmEmailHandler> logger,
        IValidator<ConfirmEmailRequest> validator)
    {
        _userManager = userManager;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Outcome> ExecuteAsync(ConfirmEmailRequest command, CancellationToken ct)
    {
        var validateResult = await _validator.ValidateAsync(command, ct);
        if (!validateResult.IsValid)
        {
            _logger.LogDebug("ConfirmEmailRequest validation failed.");
            return Outcome.Problem(validateResult.Errors
                .Select(x => new OutcomeError(x.ErrorCode, x.ErrorMessage)));
        }

        var user = await _userManager.FindByIdAsync(command.UserId);
        if (user == null)
        {
            _logger.LogWarning("User not found for ID {UserId}", command.UserId);
            return Outcome.NotFound(new OutcomeError("User not found."));
        }

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(command.Code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result?.Succeeded == true)
        {
            _logger.LogInformation("User email confirmed successfully for {Email}", LogSanitizer.SanitizeAndMask(user.Email));
            return Outcome.Success();
        }

        _logger.LogWarning("Email confirmation failed for {Email}", LogSanitizer.SanitizeAndMask(user.Email));
        return Outcome.Validation(
            result?.Errors?.Select(e => new OutcomeError(e.Code, e.Description)) ?? []
            );
    }
}

