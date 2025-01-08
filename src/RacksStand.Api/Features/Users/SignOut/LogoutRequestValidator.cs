namespace RacksStand.Api.Features.Users.SignOut;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        // Validate that AccessToken is not empty
        RuleFor(request => request.AccessToken)
            .NotEmpty().WithMessage("AccessToken is required.");

        // Validate that RefreshToken is not empty
        RuleFor(request => request.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken is required.");
    }
}
