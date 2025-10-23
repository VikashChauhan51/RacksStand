using RacksStand.Api.Endpoints.Auth.SignUp.Models;

namespace RacksStand.Api.Endpoints.Auth.SignUp.Validators;

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.AgreedTermsConfirmed)
            .Equal(true).WithMessage("Terms must be accepted.");

        RuleFor(x => x.RedirectUrl)
           .NotEmpty().WithMessage("Redirect URL is required.")
           .Must(BeAValidUrl).WithMessage("Redirect URL must be a valid absolute URL.");
    }

    private static bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

