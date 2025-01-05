namespace RacksStand.Api.Features.Users.SignIn;

public class SigninRequestValidator : AbstractValidator<SigninRequest>
{
    public SigninRequestValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty().WithMessage("Email is required.")
        .EmailAddress().WithMessage("Invalid Email format.")
        .MaximumLength(150).WithMessage("Email should be less than 150 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(100).WithMessage("Password should be less than 100 characters.");
    }
}
