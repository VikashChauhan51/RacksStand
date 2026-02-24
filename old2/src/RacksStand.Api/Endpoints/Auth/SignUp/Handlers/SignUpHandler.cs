using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.SignUp.Models;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.SignUp.Handlers;

public sealed class SignUpHandler : ICommandHandler<SignUpRequest, Outcome>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly ILogger<SignUpHandler> _logger;
    private readonly IValidator<SignUpRequest> _validator;

    public SignUpHandler(
        ILogger<SignUpHandler> logger,
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        IValidator<SignUpRequest> validator)
    {
        _userManager = userManager;
        _userStore = userStore;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Outcome> ExecuteAsync(SignUpRequest command, CancellationToken ct)
    {
        var validateResult = await _validator.ValidateAsync(command, ct);
        if (validateResult?.IsValid == false)
        {
            _logger.LogDebug("Request validation failed.");
            return Outcome.Problem(
                validateResult.Errors.Select(x => new OutcomeError(x.ErrorCode, x.ErrorMessage))
            );
        }

        var existingUser = await _userManager.FindByEmailAsync(command.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("User already exists with email {Email}.", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome.Conflict(new OutcomeError("User with this email already exists."));
        }

        var user = new ApplicationUser
        {
            UserName = command.Email,
            Email = command.Email,
            FullName = command.FullName
        };

        await _userStore.SetUserNameAsync(user, command.Email, ct);
        var result = await _userManager.CreateAsync(user, command.Password);
        if (result == null || !result.Succeeded)
        {
            _logger.LogError("Signup failed with email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome.Validation(
                result?.Errors?.Select(x => new OutcomeError(x.Code, x.Description)) ?? []
            );
        }

        _logger.LogInformation("User successfully created with email {Email}", LogSanitizer.SanitizeAndMask(command.Email));

        SignUpEmailRequest sendEmailCommand = new(user.Id, command.RedirectUrl);
        sendEmailCommand.ExecuteAsync().ConfigureAwait(false);

        return Outcome.Success();
    }
}

