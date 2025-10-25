
using RacksStand.Api.Endpoints.Auth.SignIn.Models;

namespace RacksStand.Api.Endpoints.Auth.SignIn.Validators;

public sealed class SignInRequestValidator: AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}
