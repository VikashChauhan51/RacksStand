namespace RacksStand.Api.Endpoints.Auth.ConfirmAccount.Validators;

using FluentValidation;
using RacksStand.Api.Endpoints.Auth.ConfirmAccount.Models;

public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Confirmation code is required.");
    }
}

