using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RacksStand.Api.Core.Authentication.Abstractions;
using RacksStand.Api.Core.Configurations;
using RacksStand.Api.Core.Logging;
using RacksStand.Api.Core.Resilience;
using RacksStand.Api.Domain.Entities;
using RacksStand.Api.Endpoints.Auth.SignIn.Models;
using ResultifyCore;

namespace RacksStand.Api.Endpoints.Auth.SignIn.Handlers;

public sealed class SignInHandler : ICommandHandler<SignInRequest, Outcome<SignInResponse?>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    private readonly ILogger<SignInHandler> _logger;
    private readonly IValidator<SignInRequest> _validator;
    private readonly AuthenticationConfiguration _authenticationConfiguration;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IPolicyService _policyService;

    public SignInHandler(
        ILogger<SignInHandler> logger,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserStore<ApplicationUser> userStore,
        IJwtTokenService jwtTokenService,
        IPolicyService policyService,
         IOptions<AuthenticationConfiguration> options,
        IValidator<SignInRequest> validator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
        _jwtTokenService = jwtTokenService;
        _policyService = policyService;
        _authenticationConfiguration = options?.Value ?? throw new ValidationException("Authentication configuration is missing.");
        _logger = logger;
        _validator = validator;
    }
    public async Task<Outcome<SignInResponse?>> ExecuteAsync(SignInRequest command, CancellationToken ct)
    {
        var validateResult = await _validator.ValidateAsync(command, ct);
        if (validateResult?.IsValid == false)
        {
            _logger.LogDebug("Request validation failed.");
            return Outcome<SignInResponse?>.Problem(
                validateResult.Errors.Select(x => new OutcomeError(x.ErrorCode, x.ErrorMessage))
            );
        }

        var user = await _userManager.FindByNameAsync(command.Email);
        if (user == null)
        {
            _logger.LogWarning("SignIn failed for email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome<SignInResponse?>.NotFound(new OutcomeError("User not found."));
        }

        if (user.DeletedAt != null)
        {
            _logger.LogWarning("SignIn attempt for deleted account with email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome<SignInResponse?>.NotFound(new OutcomeError("NotExists", "User not found."));

        }

        if (!user.EmailConfirmed)
        {
            _logger.LogWarning("SignIn attempt for unconfirmed email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome<SignInResponse?>.Forbidden(new OutcomeError("EmailNotConfirmed", "Email address is not confirmed."));
        }

        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
        {
            _logger.LogWarning("SignIn attempt for locked out account with email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            return Outcome<SignInResponse?>.Forbidden(new OutcomeError("AccountLocked", "User account is locked."));
        }

        var result = await _signInManager.PasswordSignInAsync(command.Email, command.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            _logger.LogWarning("SignIn failed for email {Email}", LogSanitizer.SanitizeAndMask(command.Email));

            // Increment failed attempts and lock user if necessary
            user.AccessFailedCount++;
            if (user.AccessFailedCount >= _authenticationConfiguration.MaxRetryAttempts)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(_authenticationConfiguration.LockoutDurationMinutes);
                _logger.LogWarning("User account locked due to multiple failed sign-in attempts for email {Email}", LogSanitizer.SanitizeAndMask(command.Email));
            }
            await _userStore.UpdateAsync(user, ct);
            return Outcome<SignInResponse?>.Unauthorized(new OutcomeError("InvalidCredentials", "Invalid credentials."));

        }

        user.AccessFailedCount = 0;
        user.LockoutEnabled = false;
        user.LockoutEnd = null;
        await _userStore.UpdateAsync(user, ct);
        var roles = await _userManager.GetRolesAsync(user);

        var retryPolicy = _policyService.CreateCustomPolicy(
            _authenticationConfiguration.MaxRetryAttempts,
            _authenticationConfiguration.DefaultRetryDelayMilliseconds);

        var accessToken = await retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogDebug("Attempting to generate access and refresh tokens for user {UserId}", user.Id);

            var token = await _jwtTokenService.GenerateAccessTokenAsync(user, roles ?? [], ct);
            if (token == null)
            {
                throw new InvalidOperationException("Access token generation failed.");
            }
            return token;
        });

        if (accessToken == null)
        {
            _logger.LogError("Failed to generate access token for user {UserId}", user.Id);
            return Outcome<SignInResponse?>.Problem(new OutcomeError("TokenGenerationFailed", "Failed to generate access token."));
        }
        var refreshToken = await retryPolicy.ExecuteAsync(async () =>
        {
            var token = await _jwtTokenService.GenerateRefreshTokenAsync(user, ct);
            if (token == null)
            {
                throw new InvalidOperationException("Refresh token generation failed.");
            }
            return token;
        });

        if (refreshToken == null)
        {
            _logger.LogError("Failed to generate refresh token for user {UserId}", user.Id);
            return Outcome<SignInResponse?>.Problem(new OutcomeError("TokenGenerationFailed", "Failed to generate refresh token."));
        }

        return Outcome<SignInResponse?>.Success(new SignInResponse(accessToken, refreshToken));
    }
}
