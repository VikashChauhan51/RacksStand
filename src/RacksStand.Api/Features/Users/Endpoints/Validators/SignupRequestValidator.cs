using RacksStand.Api.Features.Users.Endpoints.Models;

namespace RacksStand.Api.Features.Users.Endpoints.Validators;

public class SignupRequestValidator : AbstractValidator<SignupRequest>
{
    public SignupRequestValidator()
    {
        RuleFor(x => x.FullName)
        .NotNull()
        .NotEmpty()
        .MaximumLength(255);

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(8)
           .MaximumLength(100);

        RuleFor(x => x.PlanId)
         .NotNull()
         .NotEmpty()
        .MaximumLength(100);
    }
}
